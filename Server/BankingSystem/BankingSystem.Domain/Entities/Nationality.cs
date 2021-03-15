using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("nationality")]
    public class Nationality : BaseEntity
    {
        public Nationality()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
