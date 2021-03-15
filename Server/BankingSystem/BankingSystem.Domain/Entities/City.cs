using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("city")]
    public class City : BaseEntity
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
