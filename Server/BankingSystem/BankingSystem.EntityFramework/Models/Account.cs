using System;
using System.Collections.Generic;

namespace BankingSystem.EntityFramework.Models
{
    public class Account
    {
        public Account()
        {
            ContractCurrentAccounts = new List<Contract>();
            ContractPercentAccounts = new List<Contract>();
        }

        public int Id { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Number { get; set; }
        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }
        public ICollection<Contract> ContractCurrentAccounts { get; set; }
        public ICollection<Contract> ContractPercentAccounts { get; set; }
    }
}
