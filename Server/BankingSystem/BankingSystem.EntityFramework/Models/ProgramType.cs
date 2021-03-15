using System;
using System.Collections.Generic;

namespace BankingSystem.EntityFramework.Models
{
    public class ProgramType
    {
        public ProgramType()
        {
            Programs = new List<Program>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
