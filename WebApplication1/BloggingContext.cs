using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mingxiaoyu.Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1
{
    public class BloggingContext : DbContextBase
    {
        private readonly HttpContext _httpContext;

        public BloggingContext(DbContextOptions<BloggingContext> options, IHttpContextAccessor httpContextAccessor)
              : base(options)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public override string GetCurrentUser()
        {
            return _httpContext?.User.Identity.Name;
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
            builder.Property(x => x.Url).HasMaxLength(100);

            builder.HasData(new Blog() { Id = Guid.Parse("0B711D87-C902-4CBA-ABB1-337FBFC17E2C"), Url = "http://www.baidu.com" });
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
}

