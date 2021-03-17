using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BankingSystem.Core.Extensions;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly BankingSystemContextFactory ContextFactory;

        public Repository(BankingSystemContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entityEntry = await context.Set<T>().AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entityEntry.Entity;
        }

        public async Task<T> GetAsync(int id, IEnumerable<Expression<Func<T, object>>> propertyPaths = null)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<T>()
                .IncludeMany(propertyPaths)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(IEnumerable<Expression<Func<T, object>>> propertyPaths = null)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entities = await context.Set<T>()
                .IncludeMany(propertyPaths)
                .ToListAsync();

            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await using var context = ContextFactory.CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            await using var context = ContextFactory.CreateDbContext();
            context.Set<T>().UpdateRange(entities);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entities;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
