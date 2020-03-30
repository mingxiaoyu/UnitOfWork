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
    public class ViewRepository : ReadOnlyRepository<BlogsView>
    {
        public ViewRepository(IDbContext dbContext) : base(dbContext)
        {

        }
        public override IQueryable<BlogsView> GetQueryable()
        {
            return this.DbContext.Query<BlogsView>().FromSql("select Url from [Blogs]");
        }
    }
    public class ReadOnlyRepositoryTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture dbFixture;

        public ReadOnlyRepositoryTests(DatabaseFixture fixture)
        {
            this.dbFixture = fixture;
        }

        [Fact]
        public async Task QueryTest()
        {
            var provider = dbFixture.Provider;
            var uow = provider.GetService<IUnitOfWork>();
            var blogRepository = provider.GetService<IRepository<Blog>>();

            var bolg = new Blog() { Url = "Url" };
            var blogdb = await blogRepository.InsertAsync(bolg);
            await uow.CommitAsync();

            var viewRepository = provider.GetService<ViewRepository>();

            var list = await viewRepository.Table.ToListAsync();

            Assert.True(list != null);
            Assert.True(list.Count == 1);

            Assert.Equal("Url", list.FirstOrDefault().Url);

        }
    }
}
