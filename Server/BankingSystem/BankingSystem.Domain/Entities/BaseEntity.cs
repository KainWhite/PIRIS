using System.Collections.Generic;
using Prism.Mvvm;

namespace BankingSystem.Domain.Entities
{
    public class BaseEntity : BindableBase 
    {
        public int Id { get; set; }

        public virtual List<string> Validate()
        {
            return new List<string>();
        }
    }
}
