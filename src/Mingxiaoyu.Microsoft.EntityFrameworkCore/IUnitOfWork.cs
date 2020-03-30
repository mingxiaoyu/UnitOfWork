using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext DbContext { get; }

        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        void Commit(string who = null);

        Task CommitAsync(string who = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}