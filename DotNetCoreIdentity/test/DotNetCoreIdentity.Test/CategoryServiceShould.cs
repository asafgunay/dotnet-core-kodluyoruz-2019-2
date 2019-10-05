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
        [Fact]
        public async Task UpdateCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "Test_UpdateCategory").Options;
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            ApplicationResult<CategoryDto> resultCreate = new ApplicationResult<CategoryDto>();
            // bir yeni kategori olustur
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                var service = new CategoryService(inMemoryContext, mapper);
                CreateCategoryInput fakeCategory = new CreateCategoryInput
                {
                    CreatedById = new Guid().ToString(), // sahte kullanici
                    Name = "Lorem Ipsum",
                    UrlName = "lorem-ipsum"
                };
                resultCreate = await service.Create(fakeCategory);
            }
            ApplicationResult<CategoryDto> resultUpdate = new ApplicationResult<CategoryDto>();

            // var olan kategoriyi guncelle
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                var service = new CategoryService(inMemoryContext, mapper);

                var item = await inMemoryContext.Categories.FirstOrDefaultAsync();
                var fakeUpdate = new UpdateCategoryInput
                {
                    Id = item.Id,
                    CreatedById = item.CreatedById,
                    ModifiedById = new Guid().ToString(),
                    Name = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor"
                };
                // update servisi calistir
                resultUpdate = await service.Update(fakeUpdate);
            }
            // kontrol et
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // contextte kategori var mi?
                // create servis duzgun calisti mi?
                // update servis duzgun calisti mi?
                // update islem basarili mi (context ten gelen veri ile string ifadeleri karsilastir)
            }
        }
    }
}
