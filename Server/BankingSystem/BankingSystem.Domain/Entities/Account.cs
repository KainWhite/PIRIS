using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BankingSystem.Core.Extensions;

namespace BankingSystem.Domain.Entities
{
    [Table("account")]
    public class Account : BaseEntity
    {
        public Account()
        {
            _currentAccountContracts = new List<Contract>();
            _percentAccountContracts = new List<Contract>();
            Contracts = new List<Contract>();
        }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Number { get; set; }
        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        [InverseProperty("CurrentAccount")]
        public ICollection<Contract> CurrentAccountContracts
        {
            get => _currentAccountContracts;
            set
            {
                _currentAccountContracts = value;
                var contracts = new HashSet<Contract>(PercentAccountContracts);
                contracts.AddRange(_currentAccountContracts);
                Contracts = contracts.ToList();
            }
        }

        [InverseProperty("PercentAccount")]
        public ICollection<Contract> PercentAccountContracts
        {
            get => _percentAccountContracts;
            set
            {
                _percentAccountContracts = value;
                var contracts = new HashSet<Contract>(CurrentAccountContracts);
                contracts.AddRange(_percentAccountContracts);
                Contracts = contracts.ToList();
            }
        }

        [NotMapped]
        public ICollection<Contract> Contracts { get; set; }

        private ICollection<Contract> _currentAccountContracts;
        private ICollection<Contract> _percentAccountContracts;
    }
}
