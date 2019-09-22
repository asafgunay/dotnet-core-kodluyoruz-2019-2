using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Domain.BlogEntries;
using DotNetCoreIdentity.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreIdentity.Application.BlogServices
{
    public class PostService : IPostService
    {
        private readonly ApplicationUserDbContext _context;
        public PostService(ApplicationUserDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationResult<PostDto>> Get(Guid id)
        {
            try
            {
                Post post = await _context.Posts.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
                PostDto postDto = new PostDto
                {
                    Category = new CategoryDto
                    {
                        Id = post.Category.Id,
                        CreatedBy = post.Category.CreatedBy,
                        CreatedById = post.Category.CreatedById,
                        CreatedDate = post.Category.CreatedDate,
                        ModifiedBy = post.Category.ModifiedBy,
                        ModifiedById = post.Category.ModifiedById,
                        ModifiedDate = post.Category.ModifiedDate,
                        Name = post.Category.Name,
                        UrlName = post.Category.UrlName
                    },
                    CategoryId = post.CategoryId,
                    Content = post.Content,
                    CreatedBy = post.CreatedBy,
                    CreatedById = post.CreatedById,
                    CreatedDate = post.CreatedDate,
                    Id = post.Id,
                    ModifiedBy = post.ModifiedBy,
                    ModifiedById = post.ModifiedById,
                    ModifiedDate = post.ModifiedDate,
                    Title = post.Title,
                    UrlName = post.UrlName
                };

                return new ApplicationResult<PostDto>
                {
                    Succeeded = true,
                    Result = postDto
                };
            }
            catch (Exception ex)
            {
                return new ApplicationResult<PostDto>
                {
                    Succeeded = false,
                    Result = new PostDto(),
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApplicationResult<List<PostDto>>> GetAll()
        {
            throw new NotImplementedException();
        }



        public async Task<ApplicationResult<PostDto>> Create(CreatePostInput input)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }



        public async Task<ApplicationResult<PostDto>> Update(UpdatePostInput input)
        {
            throw new NotImplementedException();
        }
    }
}
