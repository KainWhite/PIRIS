using BankingSystem.Domain.Entities;

namespace BankingSystem.EntityFramework.Repositories
{
    public class ProgramRepository : Repository<Program>
    {
        public ProgramRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
