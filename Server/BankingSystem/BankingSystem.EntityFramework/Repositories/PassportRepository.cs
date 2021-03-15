using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class PassportRepository : Repository<Passport>
    {
        public PassportRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
