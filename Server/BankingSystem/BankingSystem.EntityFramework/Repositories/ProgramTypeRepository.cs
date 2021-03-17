using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class ProgramTypeRepository : Repository<ProgramType>
    {
        public ProgramTypeRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
