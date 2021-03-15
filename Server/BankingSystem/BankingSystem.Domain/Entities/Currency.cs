using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Domain.Entities
{
    [Table("currency")]
    public class Currency : BaseEntity
    {
        public Currency()
        {
            Programs = new List<Program>();
        }

        public string Code { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
