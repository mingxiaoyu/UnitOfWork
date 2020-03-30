using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UnitOfWork.Tests
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        public BloggingContext CreateDbContext(string[] args)
        {
            var services = new ServiceCollection();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            services.AddUnitOfWork<BloggingContext>(x => x.UseSqlite(connection), typeof(BlogServiceTests).GetTypeInfo().Assembly);

            var Provider = services.BuildServiceProvider();

            return Provider.GetService<BloggingContext>();
        }
    }
}
