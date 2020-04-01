using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        bool IsQueryType { get; }

        TEntity GetById(params object[] keyValues);

        ValueTask<TEntity> GetByIdAsync(params object[] keyValues);

        EntityEntry<TEntity> Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry<TEntity> Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        EntityEntry<TEntity> Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        EntityEntry<TEntity> Entry(TEntity entity);
    }
}