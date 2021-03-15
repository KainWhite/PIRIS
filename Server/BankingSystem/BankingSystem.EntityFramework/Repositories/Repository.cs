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
        private readonly BankingSystemContextFactory _contextFactory;

        public Repository(BankingSystemContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await using var context = _contextFactory.CreateDbContext();
            var entityEntry = await context.Set<T>().AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entityEntry.Entity;
        }

        public async Task<T> GetAsync(int id, IEnumerable<Expression<Func<T, object>>> propertyPaths = null)
        {
            await using var context = _contextFactory.CreateDbContext();
            var entity = await context.Set<T>()
                .IncludeMany(propertyPaths)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(IEnumerable<Expression<Func<T, object>>> propertyPaths = null)
        {
            await using var context = _contextFactory.CreateDbContext();
            var entities = await context.Set<T>()
                .IncludeMany(propertyPaths)
                .ToListAsync()
                .ConfigureAwait(false);

            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await using var context = _contextFactory.CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var context = _contextFactory.CreateDbContext();
            var entity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
