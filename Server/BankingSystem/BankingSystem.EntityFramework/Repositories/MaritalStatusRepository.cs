using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class MaritalStatusRepository : Repository<MaritalStatus>
    {
        public MaritalStatusRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
