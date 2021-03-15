using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeMany<T>(
            this IQueryable<T> queryable,
            IEnumerable<Expression<Func<T, object>>> propertyPaths) where T : class
        {
            if (propertyPaths == null)
            {
                return queryable;
            }

            foreach (var propertyPath in propertyPaths)
            {
                queryable = queryable.Include(propertyPath);
            }
            return queryable;
        }
    }
}
