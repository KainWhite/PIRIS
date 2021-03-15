using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class GenderRepository : Repository<Gender>
    {
        public GenderRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
