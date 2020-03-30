using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbContext DbContext { get; private set; }
        public string CurrentUser { get; set; }

        public UnitOfWork(IDbContext dbContext)
        {
            DbContext = dbContext;
            CurrentUser = DbContext.GetCurrentUser();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return this.DbContext.Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.DbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Commit(string who = null)
        {
            try
            {
                DbContext.SaveChanges(who);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public Task CommitAsync(string who = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return DbContext.SaveChangesAsync(who, cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            if (this.DbContext is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            return exception.ToString();
        }

        public virtual void Dispose() => DbContext.Dispose();
    }
}