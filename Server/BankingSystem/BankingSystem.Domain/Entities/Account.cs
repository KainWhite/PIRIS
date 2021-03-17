using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BankingSystem.Core.Enums;
using BankingSystem.Core.Extensions;

namespace BankingSystem.Domain.Entities
{
    [Table("account")]
    public class Account : BaseEntity
    {
        public Account()
        {
            CurrentAccountContracts = new List<Contract>();
            PercentAccountContracts = new List<Contract>();
        }

        public decimal Debit { get; set; } = 0;
        public decimal Credit { get; set; } = 0;
        public string Number { get; set; }
        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        [InverseProperty("CurrentAccount")]
        public List<Contract> CurrentAccountContracts { get; set; }

        [InverseProperty("PercentAccount")]
        public List<Contract> PercentAccountContracts { get; set; }

        public decimal Debt { get; set; } = 0;
        public decimal DebtPercents { get; set; } = 0;

        [NotMapped]
        public List<Contract> Contracts
        {
            get
            {
                if (_contracts == null)
                {
                    var contracts = new HashSet<Contract>(CurrentAccountContracts);
                    contracts.AddRange(PercentAccountContracts);
                    _contracts = contracts.ToList();
                }

                return _contracts;
            }
        }

        [NotMapped]
        public DateTime ContractConclusionDate { get; set; }
        
        [NotMapped]
        public DateTime ContractEndDate { get; set; }


        private List<Contract> _contracts;

        public static string CreateAccountNumber(AccountTypeEnum accountType, int contractNumber)
        {
            long result = contractNumber * 10 % 1_000_000_000;
            switch (accountType)
            {
                case AccountTypeEnum.Cashier:
                    result += 1010_000_000_000; 
                    break;
                case AccountTypeEnum.BankDevelopmentFund:
                    result += 7327_000_000_000;
                    break;
                case AccountTypeEnum.CurrentAccount:
                    result += 3014_000_000_000;
                    break;
                case AccountTypeEnum.PercentAccount:
                    result += 2400_000_000_000;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(accountType), accountType, null);
            }

            result += result.GetDigitSum() % 10;
            return result.ToString();
        }
    }
}
