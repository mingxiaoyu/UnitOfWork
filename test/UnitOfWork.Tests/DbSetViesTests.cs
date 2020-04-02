using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mingxiaoyu.Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using System.Linq;


namespace UnitOfWork.Tests
{
    public class DbSetViesTests
    {
        [Fact]
        public async Task BolgViewTest()
        {
            var services = new ServiceCollection();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            services.AddUnitOfWork<BloggingContext>(x => x.UseSqlite(connection), typeof(BlogServiceTests).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();

            var context = provider.GetService<BloggingContext>();
            context.Database.Migrate();
            var uow = provider.GetService<IUnitOfWork>();
            var blogRepository = provider.GetService<IRepository<Blog>>();
            var bolgViewRepository = provider.GetService<IRepository<NewBlogsView>>();


            await blogRepository.InsertAsync(new Blog() { Url = "Url1" });
            await blogRepository.InsertAsync(new Blog() { Url = "Url2" });
            await uow.CommitAsync();
            var list = bolgViewRepository.Table.ToList();

            Assert.True(list != null);
            Assert.True(list.Count == 2);

            list = await bolgViewRepository.Table.Where(x => x.Url == "Url1").ToListAsync();
            Assert.True(list.Count == 1);
        }
    }
}
