using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BankingSystem.Core.Enums;
using BankingSystem.Core.Extensions;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework.Repositories;
using Prism.Mvvm;

namespace BankingSystem.WPF.Models
{
    public class AccountListModel : BindableBase
    {
        public BindingList<Account> Accounts { get; set; } = new BindingList<Account>();

        public DateChange DateChange
        {
            get => _dateChange;
            set
            {
                _dateChange = value;
                RaisePropertyChanged();
            }
        }

        private readonly AccountRepository _accountRepository;
        private readonly DateChangeRepository _dateChangeRepository;
        private DateChange _dateChange;

        public AccountListModel(
            AccountRepository accountRepository,
            DateChangeRepository dateChangeRepository)
        {
            _accountRepository = accountRepository;
            _dateChangeRepository = dateChangeRepository;

            FillData();
        }

        public async Task ShiftTimeToCurrentDate(DateTime prevDate)
        {
            if (prevDate.CompareTo(DateChange.CurrentDate) == 0)
            {
                return;
            }

            var cashier = await _accountRepository.GetAccountWithType(AccountTypeEnum.Cashier);
            var bankDevelopmentFund = await _accountRepository.GetAccountWithType(AccountTypeEnum.BankDevelopmentFund);
            var accounts = (await _accountRepository.GetForTimeShift()).ToList();
            foreach (var account in accounts)
            {
                account.ContractConclusionDate = account.Contracts[0].ConclusionDate;
                account.ContractEndDate =
                    account.Contracts[0].ConclusionDate.AddMonths(account.Contracts[0].Program.TermMonthCount);
            }
            accounts = accounts.OrderByDescending(x => x.ContractEndDate).ToList();
            var depositAccounts =
                accounts.Where(x => x.Contracts[0].Program.ProgramType.Name == ProgramTypeEnum.Deposit);
            var creditAccounts =
                accounts.Where(x => x.Contracts[0].Program.ProgramType.Name == ProgramTypeEnum.Credit);
            var oneDay = TimeSpan.FromDays(1);
            var currentDay = prevDate;
            while (currentDay < DateChange.CurrentDate)
            {
                currentDay += oneDay;
                foreach (var account in depositAccounts)
                {
                    if (account.ContractEndDate < currentDay)
                    {
                        break;
                    }

                    if (account.AccountType.Name == AccountTypeEnum.PercentAccount)
                    {
                        AddDepositPercentAmount(account, bankDevelopmentFund);
                        if (currentDay.Day == account.ContractConclusionDate.Day
                            || (currentDay.IsLastDayOfMonth() && currentDay.Day < account.ContractConclusionDate.Day))
                        {
                            TakeAllPercentAccountMoney(account, cashier);
                        }
                    }
                    if (account.AccountType.Name == AccountTypeEnum.CurrentAccount)
                    {
                        if (currentDay.CompareTo(account.ContractEndDate) == 0)
                        {
                            TakeAllCurrentAccountMoney(account, cashier, bankDevelopmentFund);
                        }
                    }
                }

                foreach (var account in creditAccounts)
                {
                    if (account.ContractEndDate < currentDay)
                    {
                        break;
                    }

                    if (account.AccountType.Name == AccountTypeEnum.PercentAccount)
                    {
                        AddCreditPercentAmount(account, bankDevelopmentFund);
                        if (currentDay.Day == account.ContractConclusionDate.Day
                            || (currentDay.IsLastDayOfMonth() && currentDay.Day < account.ContractConclusionDate.Day))
                        {
                            PayDebt(account, cashier, bankDevelopmentFund);
                        }
                    }
                    if (account.AccountType.Name == AccountTypeEnum.CurrentAccount)
                    {
                        if (currentDay.CompareTo(account.ContractEndDate) == 0)
                        {
                            FinalizeDebt(account, cashier, bankDevelopmentFund);
                        }
                    }
                }
            }

            accounts.Add(cashier);
            accounts.Add(bankDevelopmentFund);
            await _accountRepository.UpdateRangeAsync(accounts);
            await _dateChangeRepository.UpdateAsync(DateChange);

            FillData();
        }

        public void FillData()
        {
            _accountRepository.GetAllIncludeAll().ContinueWith(task =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Accounts.Clear();
                    Accounts.AddRange(task.Result.OrderBy(x =>
                    {
                        if (x.Contracts.Count > 0)
                        {
                            return x.Contracts[0].User.Id;
                        }
                        return -1;
                    }));
                });
            });

            _dateChangeRepository.GetLast().ContinueWith(task => { DateChange = task.Result; });
        }

        #region deposit

        private void AddDepositPercentAmount(Account percentAccount, Account bankDevelopmentFund)
        {
            var percentAmount = (2 * percentAccount.Contracts[0].Amount - percentAccount.Contracts[0].CurrentAccount.Credit)
                * percentAccount.Contracts[0].Program.Percent / 36500;
            percentAccount.Credit += percentAmount;
            bankDevelopmentFund.Debit += percentAmount;
        }

        private void TakeAllPercentAccountMoney(Account percentAccount, Account cashier)
        {
            var percentAccountMoney = percentAccount.Credit - percentAccount.Debit;

            cashier.Debit += percentAccountMoney;
            percentAccount.Debit = percentAccount.Credit;

            cashier.Credit += percentAccountMoney;
        }

        private void TakeAllCurrentAccountMoney(Account currentAccount, Account cashier, Account bankDevelopmentFund)
        {
            var currentAccountMoney = 2 * currentAccount.Contracts[0].Amount - currentAccount.Credit;

            bankDevelopmentFund.Debit += currentAccountMoney;
            currentAccount.Credit += currentAccountMoney;

            currentAccount.Debit += currentAccountMoney;
            cashier.Debit += currentAccountMoney;

            cashier.Credit += currentAccountMoney;
        }

        #endregion

        #region credit

        private void AddCreditPercentAmount(Account percentAccount, Account bankDevelopmentFund)
        {
            var percentAmount = percentAccount.Contracts[0].CurrentAccount.Debt * percentAccount.Contracts[0].Program.Percent / 36500;
            percentAccount.DebtPercents += percentAmount;
            percentAccount.Credit += percentAmount;
            bankDevelopmentFund.Credit += percentAmount;
        }

        private void PayDebt(Account percentAccount, Account cashier, Account bankDevelopmentFund)
        {
            decimal payment = 0;
            switch (percentAccount.Contracts[0].Program.Name)
            {
                case "Credit with annuity repayment method":
                    var i = percentAccount.Contracts[0].Program.Percent / 1200;
                    var coefficient = (decimal.ToDouble(i) * Math.Pow(1 + decimal.ToDouble(i), 12)) /
                                (Math.Pow(1 + decimal.ToDouble(i), 12) - 1);
                    payment = percentAccount.Contracts[0].Amount * Convert.ToDecimal(coefficient);
                    
                    break;

                case "Credit with differential repayment method":
                    var mainPayment = percentAccount.Contracts[0].Amount /
                                      percentAccount.Contracts[0].Program.TermMonthCount;
                    var percentPayment = percentAccount.DebtPercents;
                    payment = mainPayment + percentPayment;

                    break;
            }

            cashier.Debit += payment;

            cashier.Credit += payment;
            percentAccount.Debit += percentAccount.DebtPercents;
            percentAccount.Contracts[0].CurrentAccount.Debit += payment - percentAccount.DebtPercents;

            percentAccount.Contracts[0].CurrentAccount.Credit += payment - percentAccount.DebtPercents;
            bankDevelopmentFund.Credit += payment - percentAccount.DebtPercents;

            percentAccount.Contracts[0].CurrentAccount.Debt -= payment - percentAccount.DebtPercents;
            percentAccount.DebtPercents = 0;
        }

        public void FinalizeDebt(Account currentAccount, Account cashier, Account bankDevelopmentFund)
        {

        }

        #endregion
    }
}
