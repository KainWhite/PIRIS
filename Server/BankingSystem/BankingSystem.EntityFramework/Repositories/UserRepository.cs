using System;
using System.Collections.Generic;
using System.Text;
using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
