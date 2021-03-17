using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class CurrencyRepository : Repository<Currency>
    {
        public CurrencyRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
