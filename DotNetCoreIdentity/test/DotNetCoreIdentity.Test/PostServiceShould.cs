using AutoMapper;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.BlogServices;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Application.Shared;
using DotNetCoreIdentity.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreIdentity.Test
{
    public class PostServiceShould
    {
        #region CreatePostHelpers
        // verileri kayit ederken db ayarlarinin ayni dbname icerisinde olduguna dikkat edin, eger db nameler farkliysa test verileri farkli veri tabanlarina kayit olur. tum test metotlarinda(Fact'lerde) ayri dbName'ler verilmesinin sebebi budur.
        public async Task<List<ApplicationResult<PostDto>>> CreatePost(List<CreatePostInput> fakePostList, string postDbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>();
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                CategoryServiceShould categoryShould = new CategoryServiceShould();
                result = await categoryShould.CreateCategory(inMemoryContext, mapper);
                await categoryShould.AssertCreatedCategory(inMemoryContext, result);
            }

            List<ApplicationResult<PostDto>> createdPostResulList = new List<ApplicationResult<PostDto>>();
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                var service = new PostService(inMemoryContext, mapper);
                foreach (var item in fakePostList)
                {
                    item.CategoryId = result.Result.Id;
                    createdPostResulList.Add(await service.Create(item));
                }
            }
            return createdPostResulList;

        }

        async Task AssertCreatedPostAsync(ApplicationUserDbContext inMemoryContext, List<ApplicationResult<PostDto>> resultList, List<CreatePostInput> fakePostList)
        {
            // burasi onemli
            Assert.Equal(fakePostList.Count, await inMemoryContext.Posts.CountAsync());

            foreach (var fakePost in fakePostList)
            {
                ApplicationResult<PostDto> foundResult = resultList.Find(x =>
                  x.Result.Content == fakePost.Content &&
                  x.Result.Title == fakePost.Title &&
                  x.Result.UrlName == fakePost.UrlName &&
                  x.Result.CreatedBy == fakePost.CreatedBy &&
                  x.Result.CreatedById == fakePost.CreatedById
                  );
                Assert.True(foundResult.Succeeded);
                Assert.NotNull(foundResult.Result);
                var item = await inMemoryContext.Posts.FirstAsync(x =>
                x.Content == fakePost.Content &&
                  x.Title == fakePost.Title &&
                  x.UrlName == fakePost.UrlName &&
                  x.CreatedBy == fakePost.CreatedBy &&
                  x.CreatedById == fakePost.CreatedById
                  );
                Assert.Equal(fakePost.CreatedBy, item.CreatedBy);
                Assert.Equal(fakePost.Title, item.Title);
                Assert.Equal(fakePost.UrlName, item.UrlName);
                Assert.Equal(fakePost.Content, item.Content);
            }
           
        }
        #endregion

        [Fact]
        public async Task CreateNewPost()
        {
            string postDbName = "postDbCreateNewPost";
            var optionsPost = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;

            List<CreatePostInput> fakePostList = new List<CreatePostInput>
            {
                new CreatePostInput
                {
                    Content = "Lorem Ipsum Dolor Sit Amet",
                    Title = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor",
                    CreatedBy = "Tester1",
                    CreatedById = Guid.NewGuid().ToString()
                }
            };


            var resultList = await CreatePost(fakePostList, postDbName);

            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                await AssertCreatedPostAsync(inMemoryContext, resultList, fakePostList);
            }
        }
        [Fact]
        public async Task UpdatePost()
        {
            string postDbName = "postDbUpdatePost";
            var optionsPost = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            List<CreatePostInput> fakePostList = new List<CreatePostInput>
            {
                new CreatePostInput
                {
                    Content = "Lorem Ipsum Dolor Sit Amet",
                    Title = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor",
                    CreatedBy = "Tester1",
                    CreatedById = Guid.NewGuid().ToString()
                }
            };
            ApplicationResult<PostDto> resultUpdatePost = new ApplicationResult<PostDto>();
            List<ApplicationResult<PostDto>> resultList = await CreatePost(fakePostList, postDbName);

            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                await AssertCreatedPostAsync(inMemoryContext, resultList, fakePostList);
                var item = await inMemoryContext.Posts.FirstOrDefaultAsync();
                PostService service = new PostService(inMemoryContext, mapper);
                UpdatePostInput fakeUpdate = new UpdatePostInput
                {
                    Id = item.Id,
                    CreatedById = item.CreatedById,
                    ModifiedById = Guid.NewGuid().ToString(),
                    ModifiedBy = "Tester1 Updated",
                    Content = "Lorem Ipsum Dolor Sit Amet Updated",
                    Title = "Lorem Ipsum Dolor Updated",
                    UrlName = "lorem-ipsum-dolor-updated"
                };
                resultUpdatePost = await service.Update(fakeUpdate);
            }

            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                Assert.Equal(1, await inMemoryContext.Posts.CountAsync());
                Assert.True(resultUpdatePost.Succeeded);
                Assert.NotNull(resultUpdatePost.Result);
                var item = await inMemoryContext.Posts.FirstAsync();
                Assert.Equal("Tester1", item.CreatedBy);
                Assert.Equal("Tester1 Updated", item.ModifiedBy);
                Assert.Equal("Lorem Ipsum Dolor Sit Amet Updated", item.Content);
                Assert.Equal("Lorem Ipsum Dolor Updated", item.Title);
                Assert.Equal("lorem-ipsum-dolor-updated", item.UrlName);
                Assert.Equal(resultUpdatePost.Result.ModifiedById, item.ModifiedById);
            }
        }
        [Fact]
        // Category alaninin null gelmesinin sebebi onun bilgisin baska bir veritabaninda tutulmasidir. Bu sebeple veritabani ismini ayni yaptik ve artik null gelmiyor.
        public async Task GetPost()
        {
            string postDbName = "postDbGetPost";
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var optionsPost = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;
            ApplicationResult<PostDto> resultGet = new ApplicationResult<PostDto>();
            List<CreatePostInput> fakePostList = new List<CreatePostInput>
            {
                new CreatePostInput
                {
                    Content = "Lorem Ipsum Dolor Sit Amet",
                    Title = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor",
                    CreatedBy = "Tester1",
                    CreatedById = Guid.NewGuid().ToString()
                }
            };
            var resultList = await CreatePost(fakePostList, postDbName);
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                await AssertCreatedPostAsync(inMemoryContext, resultList, fakePostList);
            }
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                var service = new PostService(inMemoryContext, mapper);
                resultGet = await service.Get(resultList[0].Result.Id);
            }
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                // get servis dogru calisti mi kontrolu
                Assert.True(resultGet.Succeeded);
                Assert.NotNull(resultGet.Result);
                Assert.Equal("Lorem Ipsum Dolor Sit Amet", resultGet.Result.Content);
                Assert.Equal("Lorem Ipsum Dolor", resultGet.Result.Title);
                Assert.Equal("lorem-ipsum-dolor", resultGet.Result.UrlName);

                var optionsCategory = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;
                using (var inMemoryContextCategory = new ApplicationUserDbContext(optionsCategory))
                {
                    Assert.Equal(1, await inMemoryContextCategory.Categories.CountAsync());
                    var item = await inMemoryContextCategory.Categories.FirstAsync();
                    Assert.Equal("Lorem Ipsum", item.Name);
                    Assert.Equal("lorem-ipsum", item.UrlName);
                }
            }
        }
        [Fact]
        public async Task DeletePost()
        {
            string postDbName = "postDbDeletePost";
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var optionsPost = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;
            ApplicationResult resultDelete = new ApplicationResult();
            List<CreatePostInput> fakePostList = new List<CreatePostInput>
            {
                new CreatePostInput
                {
                    Content = "Lorem Ipsum Dolor Sit Amet",
                    Title = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor",
                    CreatedBy = "Tester1",
                    CreatedById = Guid.NewGuid().ToString()
                }
            };
            var resultList = await CreatePost(fakePostList, postDbName);
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                await AssertCreatedPostAsync(inMemoryContext, resultList, fakePostList);
            }
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                var service = new PostService(inMemoryContext, mapper);
                resultDelete = await service.Delete(resultList[0].Result.Id);
            }
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                Assert.True(resultDelete.Succeeded);
                Assert.Null(resultDelete.ErrorMessage);
                Assert.Equal(0, await inMemoryContext.Posts.CountAsync());
            }
        }
        [Fact]
        public async Task GetAllPosts()
        {
            string postDbName = "postDbGetAllPost";
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var optionsPost = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: postDbName).Options;
            List<CreatePostInput> fakePostList = new List<CreatePostInput>
            {
                new CreatePostInput
                {
                    Content = "Lorem Ipsum Dolor Sit Amet",
                    Title = "Lorem Ipsum Dolor",
                    UrlName = "lorem-ipsum-dolor",
                    CreatedBy = "Tester1",
                    CreatedById = Guid.NewGuid().ToString()
                },
                 new CreatePostInput
                {
                    Content = "Lorem Ipsum Dolor Sit Amet2",
                    Title = "Lorem Ipsum Dolor2",
                    UrlName = "lorem-ipsum-dolor2",
                    CreatedBy = "Tester2",
                    CreatedById = Guid.NewGuid().ToString()
                }
            };
            var resultList = await CreatePost(fakePostList, postDbName);
            ApplicationResult<List<PostDto>> resultGetAll = new ApplicationResult<List<PostDto>>();

            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {
                await AssertCreatedPostAsync(inMemoryContext, resultList, fakePostList);
                var service = new PostService(inMemoryContext, mapper);
                resultGetAll = await service.GetAll();
            }
            using (var inMemoryContext = new ApplicationUserDbContext(optionsPost))
            {

                foreach (var fakePost in fakePostList)
                {
                    PostDto foundResult = resultGetAll.Result.Find(x =>
                      x.Content == fakePost.Content &&
                      x.Title == fakePost.Title &&
                      x.UrlName == fakePost.UrlName &&
                      x.CreatedBy == fakePost.CreatedBy &&
                      x.CreatedById == fakePost.CreatedById
                      );
                    Assert.NotNull(foundResult);
                }

                Assert.Equal(fakePostList.Count, await inMemoryContext.Posts.CountAsync());
                Assert.True(resultGetAll.Succeeded);
                Assert.NotNull(resultGetAll.Result);
            }
        }
    }
}
