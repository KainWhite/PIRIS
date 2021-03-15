using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("marital_status")]
    public class MaritalStatus : BaseEntity
    {
        public MaritalStatus()
        {
            Users = new HashSet<User>();
        }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
