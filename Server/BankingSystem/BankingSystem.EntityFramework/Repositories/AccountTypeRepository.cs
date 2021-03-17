using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class AccountTypeRepository : Repository<AccountType>
    {
        public AccountTypeRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
