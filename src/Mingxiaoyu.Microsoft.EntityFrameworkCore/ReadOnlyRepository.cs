using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly IDbContext DbContext;

        public ReadOnlyRepository(IDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public virtual IQueryable<TEntity> Table => GetQueryable();

        public virtual IQueryable<TEntity> TableNoTracking => Table.AsNoTracking();

        public abstract IQueryable<TEntity> GetQueryable();

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return Table.Count();
            }
            else
            {
                return Table.Count(predicate);
            }
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (predicate == null)
            {
                return Table.CountAsync(cancellationToken);
            }
            else
            {
                return Table.CountAsync(predicate, cancellationToken);
            }
        }

        [Obsolete]
        public IQueryable<TEntity> EntityFromSql(string sql, params object[] parameters)
        {
            return Table.FromSql(new RawSqlString(CreateSqlWithParameters(sql, parameters)), parameters);
        }

        private static string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue;

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }

            return sql;
        }

        public bool Exists(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
            {
                return Table.Any();
            }
            else
            {
                return Table.Any(selector);
            }
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (selector == null)
            {
                return Table.AnyAsync(cancellationToken);
            }
            else
            {
                return Table.AnyAsync(selector, cancellationToken);
            }
        }
    }
}