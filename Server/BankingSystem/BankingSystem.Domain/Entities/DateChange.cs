using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BankingSystem.Domain.Entities
{
    [Table("date_change")]
    public class DateChange : BaseEntity
    {
        public DateTime CurrentDate { get; set; }
    }
}
