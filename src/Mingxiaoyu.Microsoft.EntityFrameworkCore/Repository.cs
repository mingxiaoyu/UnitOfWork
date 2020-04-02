using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(IDbContext dbContext) : base(dbContext)
        {
        }

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Entities.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
        }

        public TEntity GetById(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        public ValueTask<TEntity> GetByIdAsync(params object[] keyValues)
        {
            return Entities.FindAsync(keyValues);
        }

        protected virtual void DbSetCheck()
        {
            if (IsQueryType)
                throw new NotSupportedException();
        }

        public bool IsQueryType
        {
            get
            {
                var model = (DbContext as DbContext).Model;
                var entityTypes = model.GetEntityTypes();
                var entityTypeOfEntity = entityTypes.First(t => t.ClrType == typeof(TEntity));

                return entityTypeOfEntity.GetKeys().Count() == 0;// entityTypeOfEntity.IsQueryType;
            }
        }

        public override IQueryable<TEntity> GetQueryable()
        {
            if (IsQueryType)
                return DbContext.Query<TEntity>();
            else
                return Entities;
        }

        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            DbSetCheck();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var result = Entities.Add(entity);
            return result;
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            DbSetCheck();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
        }

        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            DbSetCheck();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Entities.AddAsync(entity, cancellationToken);
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            DbSetCheck();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return Entities.AddRangeAsync(entities, cancellationToken);
        }

        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            DbSetCheck();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Entities.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            DbSetCheck();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.UpdateRange(entities);
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                DbSetCheck();
                if (_entities == null)
                    _entities = DbContext.Set<TEntity>();

                return _entities;
            }
        }

        public virtual EntityEntry<TEntity> Entry(TEntity entity)
        {
            return (this.DbContext as DbContext).Entry<TEntity>(entity);
        }
    }
}