using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Core.Enums;
using BankingSystem.Core.Extensions;
using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.EntityFramework.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(BankingSystemContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<Account> GetAccountWithType(AccountTypeEnum accountType)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<Account>()
                .Include(x => x.AccountType)
                .FirstOrDefaultAsync(x => x.AccountType.Name == accountType)
                .ConfigureAwait(false);

            return entity;
        }

        public async Task<Account> GetIncludeAll(int id)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<Account>()
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.User)
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.Program)
                        .ThenInclude(x => x.ProgramType)
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.CurrentAccount)
                        .ThenInclude(x => x.AccountType)
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.PercentAccount)
                        .ThenInclude(x => x.AccountType)

                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.User)
                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.Program)
                        .ThenInclude(x => x.ProgramType)
                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.CurrentAccount)
                        .ThenInclude(x => x.AccountType)
                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.PercentAccount)
                        .ThenInclude(x => x.AccountType)

                .Include(x => x.AccountType)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<Account>> GetAllIncludeAll()
        {
            await using var context = ContextFactory.CreateDbContext();
            var entities = await context.Set<Account>()
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.User)
                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.User)
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.Program)
                        .ThenInclude(x => x.ProgramType)
                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.Program)
                        .ThenInclude(x => x.ProgramType)
                .Include(x => x.AccountType)
                .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<Account>> GetForTimeShift()
        {
            await using var context = ContextFactory.CreateDbContext();
            var entities = await context.Set<Account>()
                .Include(x => x.CurrentAccountContracts)
                    .ThenInclude(x => x.Program)
                        .ThenInclude(x => x.ProgramType)
                .Include(x => x.PercentAccountContracts)
                    .ThenInclude(x => x.Program)
                        .ThenInclude(x => x.ProgramType)
                .Include(x => x.AccountType)
                .Where(x => x.AccountType.Name != AccountTypeEnum.Cashier
                            && x.AccountType.Name != AccountTypeEnum.BankDevelopmentFund)
                .ToListAsync();

            return entities;
        }
    }
}
