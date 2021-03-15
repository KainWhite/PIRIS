using System;
using System.Collections.Generic;

namespace BankingSystem.EntityFramework.Models
{
    public class AccountType
    {
        public AccountType()
        {
            Accounts = new List<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
