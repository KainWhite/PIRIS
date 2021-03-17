using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Core.Extensions;
using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.EntityFramework.Repositories
{
    public class DateChangeRepository : Repository<DateChange>
    {
        public DateChangeRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<DateChange> GetLast()
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<DateChange>()
                .OrderByDescending(x => x.Id)
                .Take(1)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return entity;
        }
    }
}
