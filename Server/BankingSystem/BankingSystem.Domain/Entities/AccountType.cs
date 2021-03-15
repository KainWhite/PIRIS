using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("account_type")]
    public class AccountType : BaseEntity
    {
        public AccountType()
        {
            Accounts = new List<Account>();
        }

        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
