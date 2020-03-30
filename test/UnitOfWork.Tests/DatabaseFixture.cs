using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UnitOfWork.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public IServiceProvider Provider { get; private set; }

        public DatabaseFixture()
        {

            var services = new ServiceCollection();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            services.AddUnitOfWork<BloggingContext>(x => x.UseSqlite(connection), typeof(BlogServiceTests).GetTypeInfo().Assembly);

            Provider = services.BuildServiceProvider();

            using (var context = Provider.GetService<BloggingContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        public void Dispose()
        {
        }
    }
}
