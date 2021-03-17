using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("contract")]
    public class Contract : BaseEntity
    {
        public int UserId { get; set; }
        public int CurrentAccountId { get; set; }
        public int PercentAccountId { get; set; }
        public int ProgramId { get; set; }
        public int Number { get; set; }
        public DateTime ConclusionDate { get; set; }
        public decimal Amount { get; set; } = 1000;

        public Account CurrentAccount { get; set; }
        public Account PercentAccount { get; set; }

        public Program Program
        {
            get => _program;
            set
            {
                _program = value;
                RaisePropertyChanged();
            }
        }

        public User User { get; set; }

        private Program _program;

        public override List<string> Validate()
        {
            var errors = base.Validate();

            if (Program == null)
            {
                errors.Add("Contract program should be chosen");
            }

            return errors;
        }
    }
}
