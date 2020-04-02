using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mingxiaoyu.Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork.Tests
{
    public class BloggingContext : DbContextBase
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
              : base(options)
        {

        }

        public override string GetCurrentUser()
        {
            return "CurrentUser";
        }
    }

    public class Blog : TrackedAndSoftDelete
    {
        public string Url { get; set; }
    }

    public class BlogMap : EntityTypeConfiguration<Blog>
    {
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");
            builder.Property(x => x.Url).HasMaxLength(10);
            base.Configure(builder);
        }
    }

    public class User : EntityBase, IIgnoreIdAutoAutoGenerate
    {
        public string Name { get; set; }
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            base.Configure(builder);
        }
    }
    public class BlogsView
    {
        public string Url { get; set; }
    }

    public class BlogsViewMap : QueryTypeConfiguration<BlogsView>
    {
        public override void Configure(QueryTypeBuilder<BlogsView> builder)
        {
            builder.ToView("BolgViews");
            base.Configure(builder);
        }
    }

    public class NewBlogsView
    {
        public string Url { get; set; }
    }

    public class NewBlogsViewMap : EntityTypeConfiguration<NewBlogsView>
    {
        public override void Configure(EntityTypeBuilder<NewBlogsView> builder)
        {
            builder.HasNoKey();
            builder.ToView("NewBolgViews");
            base.Configure(builder);
        }
    }
}

