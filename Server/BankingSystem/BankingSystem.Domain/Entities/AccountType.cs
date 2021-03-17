using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BankingSystem.Core.Enums;

namespace BankingSystem.Domain.Entities
{
    [Table("account_type")]
    public class AccountType : BaseEntity
    {
        public AccountType()
        {
            Accounts = new List<Account>();
        }

        public AccountType(AccountTypeEnum accountType) : this()
        {
            Name = accountType;
        }

        public AccountTypeEnum Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
