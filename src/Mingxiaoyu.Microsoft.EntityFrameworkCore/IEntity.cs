using System;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public interface IEntity : IEntity<Guid>
    {
    }

    public interface IEntity<out TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }

    public interface ITrackedEntity
    {
        DateTimeOffset Created { get; }
        string CreatedUser { get; }
        DateTimeOffset? Modified { get; }
        string ModifiedUser { get; }
    }

    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }

    public interface IIgnoreIdAutoAutoGenerate
    {
    }
}