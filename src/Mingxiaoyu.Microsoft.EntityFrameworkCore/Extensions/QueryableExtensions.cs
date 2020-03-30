using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IPagedList<T> ToPaginatedList<T>(this IQueryable<T> query, int pageIndex = 1, int pageSize = 10) where T : class
        {
            return new PagedList<T>(query, pageIndex, pageSize);
        }

        public static async Task<IPagedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var list = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            var totalCount = await query.CountAsync(cancellationToken);
            return new PagedList<T>(list, pageIndex, pageSize, totalCount);
        }
    }
}