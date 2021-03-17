using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BankingSystem.Core.Enums;

namespace BankingSystem.Domain.Entities
{
    [Table("program_type")]
    public class ProgramType : BaseEntity
    {
        public ProgramType()
        {
            Programs = new List<Program>();
        }

        public ProgramTypeEnum Name { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
