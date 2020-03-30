using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public interface IDbContext : IDisposable
    {
        string GetCurrentUser();

        DatabaseFacade Database { get; }

        int SaveChanges(string who = null);

        Task<int> SaveChangesAsync(string who = null,CancellationToken cancellationToken = default(CancellationToken));

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbQuery<TQuery> Query<TQuery>() where TQuery : class;
    }
}