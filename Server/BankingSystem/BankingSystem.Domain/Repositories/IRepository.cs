using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);

        Task<T> GetAsync(int id, IEnumerable<Expression<Func<T, object>>> propertyPaths = null);

        Task<IEnumerable<T>> GetAllAsync(IEnumerable<Expression<Func<T, object>>> propertyPaths = null);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);
    }
}
