using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class NationalityRepository : Repository<Nationality>
    {
        public NationalityRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
