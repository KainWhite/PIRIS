using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BankingSystem.Core.Extensions;
using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.EntityFramework.Repositories
{
    public class ContractRepository : Repository<Contract>
    {
        public ContractRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<Contract> GetWithProgramAndProgramTypeAsync(int id)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<Contract>()
                .Include(x => x.Program)
                    .ThenInclude(x => x.ProgramType)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            return entity;
        }
    }
}
