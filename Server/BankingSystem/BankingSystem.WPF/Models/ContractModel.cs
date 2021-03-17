using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework.Repositories;
using Prism.Mvvm;

namespace BankingSystem.WPF.Models
{
    public class ContractModel : BindableBase
    {
        #region public properties
        public Contract Contract
        {
            get => _contract;
            set
            {
                _contract = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Program> Programs
        {
            get => _programs;
            set
            {
                _programs = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region private

        private Contract _contract;
        private IEnumerable<Program> _programs;

        private readonly ContractRepository _contractRepository;
        private readonly ProgramRepository _programRepository;
        private readonly AccountRepository _accountRepository;
        private readonly DateChangeRepository _dateChangeRepository;

        #endregion

        public ContractModel(
            ContractRepository contractRepository,
            ProgramRepository programRepository,
            AccountRepository accountRepository,
            DateChangeRepository dateChangeRepository)
        {
            _contractRepository = contractRepository;
            _programRepository = programRepository;
            _accountRepository = accountRepository;
            _dateChangeRepository = dateChangeRepository;

            FillData();
        }

        public async Task CreateContract()
        {
            var currentAccount = new Account()
            {
                AccountTypeId = (int)AccountTypeEnum.CurrentAccount,
                Number = Account.CreateAccountNumber(AccountTypeEnum.CurrentAccount, Contract.Number),
            };
            var percentAccount = new Account()
            {
                AccountTypeId = (int)AccountTypeEnum.PercentAccount,
                Number = Account.CreateAccountNumber(AccountTypeEnum.PercentAccount, Contract.Number),
            };
            var lastDateChange = await _dateChangeRepository.GetLast();

            Contract.CurrentAccount = currentAccount;
            Contract.PercentAccount = percentAccount;
            Contract.ConclusionDate = lastDateChange.CurrentDate;
            Contract.ProgramId = Contract.Program.Id;
            Contract.Program = null;

            Contract = await _contractRepository.CreateAsync(Contract);
            Contract = await _contractRepository.GetWithProgramAndProgramTypeAsync(Contract.Id);

            var cashier = await _accountRepository.GetAccountWithType(AccountTypeEnum.Cashier);
            var bankDevelopmentFund = await _accountRepository.GetAccountWithType(AccountTypeEnum.BankDevelopmentFund);

            switch (Contract.Program.ProgramType.Name)
            {
                case ProgramTypeEnum.Deposit:
                    cashier.Debit += Contract.Amount;

                    cashier.Credit += Contract.Amount;
                    currentAccount.Credit += Contract.Amount;

                    currentAccount.Debit += Contract.Amount;
                    bankDevelopmentFund.Credit += Contract.Amount;
                    break;

                case ProgramTypeEnum.Credit:
                    currentAccount.Debt += Contract.Amount;

                    bankDevelopmentFund.Debit += Contract.Amount;
                    currentAccount.Debit += Contract.Amount;

                    currentAccount.Credit += Contract.Amount;
                    cashier.Debit += Contract.Amount;

                    cashier.Credit += Contract.Amount;
                    break;
            }

            await Task.WhenAll(
                _accountRepository.UpdateAsync(cashier),
                _accountRepository.UpdateAsync(bankDevelopmentFund),
                _accountRepository.UpdateAsync(currentAccount));
        }

        public void FillData()
        {
            Contract = new Contract();

            _programRepository.GetAllAsync(new List<Expression<Func<Program, object>>>
            {
                x => x.Currency,
                x => x.ProgramType,
            }).ContinueWith(task => Programs = task.Result);
        }
    }
}
