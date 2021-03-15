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
        public decimal Amount { get; set; }

        public Account CurrentAccount { get; set; }
        public Account PercentAccount { get; set; }
        public Program Program { get; set; }
        public User User { get; set; }
    }
}
