using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    [Obsolete("Use EntityTypeConfiguration<TEntity> instead")]
    public class QueryTypeConfiguration<TEntity> : IMappingConfiguration, IQueryTypeConfiguration<TEntity> where TEntity : class
    {
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        protected virtual void PostConfigure(QueryTypeBuilder<TEntity> builder)
        {
        }

        public virtual void Configure(QueryTypeBuilder<TEntity> builder)
        {
            this.PostConfigure(builder);
        }
    }
}