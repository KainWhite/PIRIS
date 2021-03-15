using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class CityRepository : Repository<City>
    {
        public CityRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
