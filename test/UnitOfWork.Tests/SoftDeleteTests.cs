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
    public class SoftDeleteTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture dbFixture;

        public SoftDeleteTests(DatabaseFixture fixture)
        {
            this.dbFixture = fixture;
        }

        [Fact]
        public async Task SoftDeleteTest()
        {
            var provider = dbFixture.Provider;
            var uow = provider.GetService<IUnitOfWork>();
            var blogRepository = provider.GetService<IRepository<Blog>>();

            var bolg = new Blog() { Url = "Url" };
            var blogdb = await blogRepository.InsertAsync(bolg);
            await uow.CommitAsync();

            var entity = await blogRepository.Table.FirstOrDefaultAsync();
            blogRepository.Delete(entity);
            await uow.CommitAsync();

            var count = await blogRepository.Table.CountAsync();
            Assert.True(count == 0);

            count = await blogRepository.Table.IgnoreQueryFilters().CountAsync();
            Assert.True(count == 1);

        }
    }
}
