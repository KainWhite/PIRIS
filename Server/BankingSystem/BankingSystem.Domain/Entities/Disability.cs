using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("disability")]
    public class Disability : BaseEntity
    {
        public Disability()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
