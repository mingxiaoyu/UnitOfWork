using System;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public abstract class EntityBase : EntityBase<Guid>
    {
    }

    public abstract class EntityBase<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as EntityBase<TKey>);
        }

        public virtual bool Equals(EntityBase<TKey> obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if ((object)obj == null)
            {
                return false;
            }
            return Id.Equals(obj.Id) && !Equals(Id, default(TKey));
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(TKey)))
            {
                return base.GetHashCode();
            }
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            // compare references to the same memory address
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Id.Equals(b.Id) && !Equals(a.Id, default(TKey));
        }

        public static bool operator !=(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            return !(a == b);
        }
    }

    public abstract class Tracked : Tracked<Guid>
    {
    }

    public abstract class Tracked<TKey> : EntityBase<TKey>, ITrackedEntity where TKey : IEquatable<TKey>
    {
        private DateTimeOffset _created;
        private string _createdUser;
        private DateTimeOffset? _modified;
        private string _modifiedUser;

        public DateTimeOffset Created => _created;
        public string CreatedUser => _createdUser;
        public DateTimeOffset? Modified => _modified;
        public string ModifiedUser => _modifiedUser;
    }

    public abstract class TrackedAndSoftDelete : TrackedAndSoftDelete<Guid>
    {
    }

    public abstract class TrackedAndSoftDelete<TKey> : Tracked<TKey>, ISoftDeletable where TKey : IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }
    }
}