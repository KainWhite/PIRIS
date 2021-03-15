using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("gender")]
    public class Gender : BaseEntity
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
