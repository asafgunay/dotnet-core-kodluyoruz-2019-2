using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Domain.Identity;
using DotNetCoreIdentity.Domain.PostTypes;
using DotNetCoreIdentity.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreIdentity.Application
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationUserDbContext _context;
        // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationUserDbContext context, /*UserManager<ApplicationUser> userManager,*/ IMapper mapper)
        {
            _context = context;
            // _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationResult<CategoryDto>> Create(CreateCategoryInput input)
        {
            try
            {
                //var user = await _userManager.FindByIdAsync(input.CreatedById);
                Category mapCat = _mapper.Map<Category>(input);
                mapCat.CreatedById = input.CreatedById;
                //mapCat.CreatedBy = user.UserName;
                _context.Categories.Add(mapCat);
                await _context.SaveChangesAsync();
                ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>
                {
                    // haftaya bu kod yokalacak yerine automapper metodu gelecek!
                    // AutoMapper ile Category sinifini CategoryDto sinifina donusturebiliriz.
                    //Result = new CategoryDto
                    //{
                    //    CreatedById = category.CreatedById,
                    //    CreatedDate = category.CreatedDate,
                    //    CreatedBy = category.CreatedBy,
                    //    Id = category.Id,
                    //    Name = category.Name,
                    //    UrlName = category.UrlName
                    //},
                    Result = _mapper.Map<CategoryDto>(mapCat),
                    Succeeded = true
                };

                return result;
            }
            catch (Exception ex)
            {
                return new ApplicationResult<CategoryDto>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };

            }


        }

        public async Task<ApplicationResult> Delete(int Id)
        {
            try
            {
                var willDelete = await _context.Categories.FindAsync(Id);
                if (willDelete != null)
                {
                    _context.Categories.Remove(willDelete);
                    await _context.SaveChangesAsync();
                    return new ApplicationResult { Succeeded = true };
                }
                else
                {
                    return new ApplicationResult { Succeeded = false, ErrorMessage = "Bir hata oluştu lütfen kontrol edip tekrar deneyi" };

                }
            }
            catch (Exception ex)
            {
                return new ApplicationResult { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApplicationResult<CategoryDto>> Get(int Id)
        {
            try
            {
                Category category = await _context.Categories.FindAsync(Id);
                CategoryDto dto = new CategoryDto
                {
                    CreatedBy = category.CreatedBy,
                    CreatedById = category.CreatedById,
                    CreatedDate = category.CreatedDate,
                    Id = category.Id,
                    ModifiedBy = category.ModifiedBy,
                    ModifiedById = category.ModifiedById,
                    ModifiedDate = category.ModifiedDate,
                    Name = category.Name,
                    UrlName = category.UrlName
                };
                return new ApplicationResult<CategoryDto>
                {
                    Result = dto,
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                return new ApplicationResult<CategoryDto>
                {
                    Result = new CategoryDto(),
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public async Task<ApplicationResult<List<CategoryDto>>> GetAll()
        {
            try
            {
                List<CategoryDto> list = await _context.Categories.Select(category => new CategoryDto
                {
                    CreatedBy = category.CreatedBy,
                    CreatedById = category.CreatedById,
                    CreatedDate = category.CreatedDate,
                    Id = category.Id,
                    ModifiedBy = category.ModifiedBy,
                    ModifiedById = category.ModifiedById,
                    ModifiedDate = category.ModifiedDate,
                    Name = category.Name,
                    UrlName = category.UrlName
                }).ToListAsync();
                return new ApplicationResult<List<CategoryDto>>
                {
                    Result = list,
                    Succeeded = true
                };

            }
            catch (Exception ex)
            {
                return new ApplicationResult<List<CategoryDto>>
                {
                    Result = new List<CategoryDto>(),
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryInput input)
        {
            try
            {
                // var modifierUser = await _userManager.FindByIdAsync(input.ModifiedById);
                var getExistCategory = await _context.Categories.FindAsync(input.Id);
                getExistCategory.Name = input.Name;
                getExistCategory.UrlName = input.UrlName;
                getExistCategory.ModifiedBy = input.ModifiedBy;
                getExistCategory.ModifiedById = input.ModifiedById;
                getExistCategory.ModifiedDate = DateTime.UtcNow;
                _context.Update(getExistCategory);
                await _context.SaveChangesAsync();

                return new ApplicationResult<CategoryDto>
                {
                    Succeeded = true,
                    Result = new CategoryDto
                    {
                        CreatedBy = getExistCategory.CreatedBy,
                        CreatedById = getExistCategory.CreatedById,
                        CreatedDate = getExistCategory.CreatedDate,
                        Id = getExistCategory.Id,
                        ModifiedBy = getExistCategory.ModifiedBy,
                        ModifiedById = getExistCategory.ModifiedById,
                        ModifiedDate = getExistCategory.ModifiedDate,
                        Name = getExistCategory.Name,
                        UrlName = getExistCategory.UrlName
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApplicationResult<CategoryDto>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message,
                    Result = new CategoryDto()
                };

            }

        }
    }
}
