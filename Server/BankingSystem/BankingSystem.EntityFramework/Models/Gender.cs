using System;
using System.Collections.Generic;

#nullable disable

namespace BankingSystem.EntityFramework.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
