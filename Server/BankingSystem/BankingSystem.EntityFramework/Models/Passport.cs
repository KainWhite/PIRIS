using System;
using System.Collections.Generic;

#nullable disable

namespace BankingSystem.EntityFramework.Models
{
    public partial class Passport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Authority { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string IdentificationNumber { get; set; }

        public virtual User User { get; set; }
    }
}
