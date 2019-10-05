using AutoMapper;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Application.Shared;
using DotNetCoreIdentity.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
namespace DotNetCoreIdentity.Test
{
    public class CategoryServiceShould
    {
        [Fact]
        public async Task CreateNewCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "Test_NewCategoryCreate").Options;
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>();
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                var service = new CategoryService(inMemoryContext, mapper);
                CreateCategoryInput fakeCategory = new CreateCategoryInput
                {
                    CreatedById = new Guid().ToString(), // sahte kullanici
                    Name = "Lorem Ipsum",
                    UrlName = "lorem-ipsum"
                };
                result = await service.Create(fakeCategory);
            }

            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                Assert.True(result.Succeeded);
                Assert.NotNull(result.Result);
                Assert.Equal(1, await inMemoryContext.Categories.CountAsync());
                var item = await inMemoryContext.Categories.FirstOrDefaultAsync();
                Assert.Equal("Lorem Ipsum", item.Name);
                Assert.Equal("lorem-ipsum", item.UrlName);
                Assert.Equal(result.Result.CreatedById, item.CreatedById);
            }



        }
    }
}
