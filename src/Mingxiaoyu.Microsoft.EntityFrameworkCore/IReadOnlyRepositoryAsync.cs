using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public partial interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}