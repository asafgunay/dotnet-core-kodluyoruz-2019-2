using AutoMapper;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.BlogServices;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Application.Shared;
using DotNetCoreIdentity.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreIdentity.Test
{
    public class PostServiceShould
    {
        [Fact]
        public async Task CreatePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "Test_PostCreate").Options;
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            ApplicationResult<CategoryDto> resultCreateCategory = new ApplicationResult<CategoryDto>();
            // create category
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                CategoryServiceShould categoryShould = new CategoryServiceShould();
                resultCreateCategory = await categoryShould.CreateCategory(inMemoryContext, mapper);
            }
            // check create category
            ApplicationResult<PostDto> resultPost = new ApplicationResult<PostDto>();
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                CategoryServiceShould categoryShould = new CategoryServiceShould();
                await categoryShould.AssertCreatedCategory(inMemoryContext, resultCreateCategory);
                // create post
                var service = new PostService(inMemoryContext, mapper);
                CreatePostInput fakePost = new CreatePostInput
                {
                    CategoryId = resultCreateCategory.Result.Id,
                    Content = "Lorem Ipsum Dolor Sit Amet",
                    Title = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor",
                    CreatedBy = "Tester1",
                    CreatedById = Guid.NewGuid().ToString()
                };
                resultPost = await service.Create(fakePost);
            }
            // check post create service
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                Assert.True(resultPost.Succeeded);
                Assert.NotNull(resultPost.Result);
                Assert.Equal(1, await inMemoryContext.Posts.CountAsync());
                var item = await inMemoryContext.Posts.FirstAsync();
                Assert.Equal("Tester1", item.CreatedBy);
                Assert.Equal("Lorem Ipsum Dolor", item.Title);
                Assert.Equal("lorem-ipsum-dolor", item.UrlName);
                Assert.Equal("Lorem Ipsum Dolor Sit Amet", item.Content);
                Assert.Equal(resultCreateCategory.Result.Id, item.CategoryId);
            }
        }
    }
}
