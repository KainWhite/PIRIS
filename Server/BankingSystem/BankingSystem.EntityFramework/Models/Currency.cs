using System;
using System.Collections.Generic;

namespace BankingSystem.EntityFramework.Models
{
    public class Currency
    {
        public Currency()
        {
            Programs = new List<Program>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
