using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Core;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework.Repositories;
using Prism.Mvvm;

namespace BankingSystem.WPF.Models
{
    public class AccountModel : BindableBase
    {
        public Account Account
        {
            get => _account;
            set
            {
                _account = value;
                RaisePropertyChanged();
            }
        }

        public Account RelatedAccount
        {
            get => _relatedAccount;
            set
            {
                _relatedAccount = value;
                RaisePropertyChanged();
            }
        }

        public BindingList<DebtScheduleItem> DebtSchedule { get; set; } = new BindingList<DebtScheduleItem>();

        public bool ShowDebtSchedule
        {
            get => _showDebtSchedule;
            set
            {
                _showDebtSchedule = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowWithdraw
        {
            get => _showWithdraw;
            set
            {
                _showWithdraw = value;
                RaisePropertyChanged();
            }
        }

        private Account _account;
        private Account _relatedAccount;
        private bool _showDebtSchedule = false;
        private bool _showWithdraw = false;

        private readonly AccountRepository _accountRepository;
        
        public AccountModel(int accountId, AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            FillData(accountId);
        }

        public async Task WithdrawAccount()
        {
            var cashier = await _accountRepository.GetAccountWithType(AccountTypeEnum.Cashier);
            var bankDevelopmentFund = await _accountRepository.GetAccountWithType(AccountTypeEnum.BankDevelopmentFund);

            var percentAccountMoney = RelatedAccount.Credit - RelatedAccount.Debit;
            cashier.Debit += percentAccountMoney;
            RelatedAccount.Debit = RelatedAccount.Credit;

            cashier.Credit += percentAccountMoney;

            var currentAccountMoney = Account.Contracts[0].Amount;
            bankDevelopmentFund.Debit += currentAccountMoney;
            Account.Credit += currentAccountMoney;

            Account.Debit += currentAccountMoney;
            cashier.Debit += currentAccountMoney;

            cashier.Credit += currentAccountMoney;

            var accounts = new[]
            {
                cashier,
                bankDevelopmentFund,
                Account,
                RelatedAccount
            };

            await _accountRepository.UpdateRangeAsync(accounts);

            FillData(Account.Id);
        }

        public void FillData(int accountId)
        {
            DebtSchedule.Clear();
            _accountRepository.GetIncludeAll(accountId).ContinueWith(task =>
            {
                ShowWithdraw = false;
                ShowDebtSchedule = false;
                Account = task.Result;
                if (Account.AccountType.Name == AccountTypeEnum.PercentAccount)
                {
                    RelatedAccount = Account.Contracts[0].CurrentAccount;
                }
                if (Account.AccountType.Name == AccountTypeEnum.CurrentAccount)
                {
                    RelatedAccount = Account.Contracts[0].PercentAccount;
                    if (Account.Contracts[0].Program.Name == "Revocable deposit")
                    {
                        ShowWithdraw = true;
                    }
                }

                if (Account.AccountType.Name == AccountTypeEnum.CurrentAccount
                    || Account.AccountType.Name == AccountTypeEnum.PercentAccount)
                {
                    if (Account.Contracts[0].Program.Name == "Credit with annuity repayment method")
                    {
                        var x = Account.Contracts[0].Program.Percent / 1200;
                        var coefficient = (decimal.ToDouble(x) * Math.Pow(1 + decimal.ToDouble(x), 12)) /
                                          (Math.Pow(1 + decimal.ToDouble(x), 12) - 1);
                        var payment = Account.Contracts[0].Amount * Convert.ToDecimal(coefficient);

                        for (int i = 0; i < Account.Contracts[0].Program.TermMonthCount; i++)
                        {
                            DebtSchedule.Add(new DebtScheduleItem
                            {
                                Date = Account.Contracts[0].ConclusionDate.AddMonths(i + 1),
                                Amount = payment,
                            });
                        }

                        ShowDebtSchedule = true;
                    }
                    else if (Account.Contracts[0].Program.Name == "Credit with differential repayment method")
                    {
                        var mainPayment = Account.Contracts[0].Amount / Account.Contracts[0].Program.TermMonthCount;

                        for (int i = 0; i < Account.Contracts[0].Program.TermMonthCount; i++)
                        {
                            DebtSchedule.Add(new DebtScheduleItem
                            {
                                Date = Account.Contracts[0].ConclusionDate.AddMonths(i + 1),
                                Amount = mainPayment + (Account.Contracts[0].Amount - mainPayment * i)
                                    * Account.Contracts[0].Program.Percent / 1200,
                            });
                        }

                        ShowDebtSchedule = true;
                    }
                }
                    
            });
        }
    }
}
