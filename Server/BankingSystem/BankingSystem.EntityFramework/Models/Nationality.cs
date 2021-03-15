using System;
using System.Collections.Generic;

#nullable disable

namespace BankingSystem.EntityFramework.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
