using System;
using System.Linq;
using System.Linq.Expressions;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public partial interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        bool Exists(Expression<Func<TEntity, bool>> selector = null);

        IQueryable<TEntity> EntityFromSql(string sql, params object[] parameters);
    }
}