using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Domain.BlogEntries;
using DotNetCoreIdentity.Domain.Identity;
using DotNetCoreIdentity.Domain.PostTypes;
using DotNetCoreIdentity.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreIdentity.Application.BlogServices
{
    public class PostService : IPostService
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PostService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationResult<PostDto>> Get(Guid id)
        {
            try
            {
                Post post = await _context.Posts.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
                PostDto postDto = _mapper.Map<PostDto>(post);
                //PostDto postDto = new PostDto
                //{
                //    Category = new CategoryDto
                //    {
                //        Id = post.Category.Id,
                //        CreatedBy = post.Category.CreatedBy,
                //        CreatedById = post.Category.CreatedById,
                //        CreatedDate = post.Category.CreatedDate,
                //        ModifiedBy = post.Category.ModifiedBy,
                //        ModifiedById = post.Category.ModifiedById,
                //        ModifiedDate = post.Category.ModifiedDate,
                //        Name = post.Category.Name,
                //        UrlName = post.Category.UrlName
                //    },
                //    CategoryId = post.CategoryId,
                //    Content = post.Content,
                //    CreatedBy = post.CreatedBy,
                //    CreatedById = post.CreatedById,
                //    CreatedDate = post.CreatedDate,
                //    Id = post.Id,
                //    ModifiedBy = post.ModifiedBy,
                //    ModifiedById = post.ModifiedById,
                //    ModifiedDate = post.ModifiedDate,
                //    Title = post.Title,
                //    UrlName = post.UrlName
                //};

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
            try
            {
                List<Post> listRaw = await _context.Posts.Include(x => x.Category).ToListAsync();
                List<PostDto> list = _mapper.Map<List<PostDto>>(listRaw);
                //List<PostDto> list = await _context.Posts.Select(post => new PostDto
                //{
                //    Category = new CategoryDto
                //    {
                //        Id = post.Category.Id,
                //        CreatedBy = post.Category.CreatedBy,
                //        CreatedById = post.Category.CreatedById,
                //        CreatedDate = post.Category.CreatedDate,
                //        ModifiedBy = post.Category.ModifiedBy,
                //        ModifiedById = post.Category.ModifiedById,
                //        ModifiedDate = post.Category.ModifiedDate,
                //        Name = post.Category.Name,
                //        UrlName = post.Category.UrlName
                //    },
                //    CategoryId = post.CategoryId,
                //    Content = post.Content,
                //    CreatedBy = post.CreatedBy,
                //    CreatedById = post.CreatedById,
                //    CreatedDate = post.CreatedDate,
                //    Id = post.Id,
                //    ModifiedBy = post.ModifiedBy,
                //    ModifiedById = post.ModifiedById,
                //    ModifiedDate = post.ModifiedDate,
                //    Title = post.Title,
                //    UrlName = post.UrlName
                //}).ToListAsync();


                return new ApplicationResult<List<PostDto>>
                {
                    Succeeded = true,
                    Result = list
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<List<PostDto>>
                {
                    ErrorMessage = e.Message,
                    Result = new List<PostDto>(),
                    Succeeded = false
                };
            }

        }



        public async Task<ApplicationResult<PostDto>> Create(CreatePostInput input)
        {
            try
            {
                // useri al
                var user = await _userManager.FindByIdAsync(input.CreatedById);
                // maple
                Post newPost = _mapper.Map<Post>(input);
                newPost.CreatedBy = user.UserName;
                // context e ekle
                await _context.Posts.AddAsync(newPost);
                // kaydet
                await _context.SaveChangesAsync();
                // ve dto'yu maple ve return et
                return await Get(newPost.Id);
            }
            catch (Exception ex)
            {
                return new ApplicationResult<PostDto>
                {
                    Result = new PostDto(),
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            try
            {
                Post willDeletePost = await _context.Posts.FindAsync(id);
                _context.Posts.Remove(willDeletePost);
                await _context.SaveChangesAsync();
                return new ApplicationResult
                {
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                return new ApplicationResult
                {
                    ErrorMessage = ex.Message,
                    Succeeded = false
                };
            }
        }

        public async Task<ApplicationResult<PostDto>> Update(UpdatePostInput input)
        {
            try
            {
                var getExistPost = await _context.Posts.FindAsync(input.Id);
                // hata var mi kontrol ediyoruz varsa demekki boyle bir post yok
                if (getExistPost == null)
                {
                    return new ApplicationResult<PostDto>
                    {
                        Result = new PostDto(),
                        Succeeded = false,
                        ErrorMessage = "Böyle bir Post bulunamadı"
                    };
                }
                // useri al
                var user = await _userManager.FindByIdAsync(input.CreatedById);
                getExistPost.ModifiedBy = user.UserName;
                _mapper.Map(input, getExistPost);
                _context.Update(getExistPost);
                await _context.SaveChangesAsync();
                return await Get(getExistPost.Id);
            }
            catch (Exception ex)
            {
                return new ApplicationResult<PostDto>
                {
                    Result = new PostDto(),
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApplicationResult<PostDto>> GetByUrl(string categoryUrl, string postUrl)
        {
            try
            {
                var category = await _context.Categories.Where(c => c.UrlName == categoryUrl).FirstOrDefaultAsync();
                if (category != null)
                {
                    var post = await _context.Posts.Where(p => p.CategoryId == category.Id && p.UrlName == postUrl).FirstOrDefaultAsync();
                    if (post != null)
                    {
                        return new ApplicationResult<PostDto>
                        {
                            Succeeded = true,
                            Result = _mapper.Map<PostDto>(post)
                        };
                    }
                }
                return new ApplicationResult<PostDto>
                {
                    Result = new PostDto(),
                    Succeeded = false,
                    ErrorMessage = "Boyle bir icerik bulunamadi."
                };

            }
            catch (Exception ex)
            {
                return new ApplicationResult<PostDto>
                {
                    Result = new PostDto(),
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public async Task<ApplicationResult> UpdateImageUrl(Guid id, string filePath)
        {
            try
            {
                var existPost = await _context.Posts.FindAsync(id);
                existPost.ImageUrl = filePath;
                await _context.SaveChangesAsync();
                return new ApplicationResult
                {
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }

}
