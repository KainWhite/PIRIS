using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class DisabilityRepository : Repository<Disability>
    {
        public DisabilityRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
