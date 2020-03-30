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

    public class RepositoryTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture dbFixture;

        public RepositoryTests(DatabaseFixture fixture)
        {
            this.dbFixture = fixture;
        }

        [Fact]
        public async Task CURDTest()
        {

            var provider = dbFixture.Provider;

            var uow = provider.GetService<IUnitOfWork>();
            var blogRepository = provider.GetService<BlogRepository>();

            var bolg = new Blog() { Url = "Url" };
            var blogdb = await blogRepository.InsertAsync(bolg);
            await uow.CommitAsync();

            var list = await blogRepository.Table.ToListAsync();

            Assert.True(list != null);
            Assert.True(list.Count == 1);

            Assert.Equal("Url", list.FirstOrDefault().Url);

            var entity = await blogRepository.Table.FirstOrDefaultAsync();

            entity.Url = "Url2";
            blogRepository.Update(entity);
            await uow.CommitAsync();

            entity = await blogRepository.Table.FirstOrDefaultAsync();
            Assert.Equal("Url2", entity.Url);

            blogRepository.Delete(entity);
            await uow.CommitAsync();

            list = await blogRepository.Table.ToListAsync();
            Assert.True(list.Count == 0);

        }
    }
}
