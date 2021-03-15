using System;
using System.Collections.Generic;

namespace BankingSystem.EntityFramework.Models
{
    public class Program
    {
        public Program()
        {
            Contracts = new List<Contract>();
        }

        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int ProgramTypeId { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }
        public int TermMonthCount { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Currency Currency { get; set; }
        public ProgramType ProgramType { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
