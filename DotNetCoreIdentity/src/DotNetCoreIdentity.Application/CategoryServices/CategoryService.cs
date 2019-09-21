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
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoryService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationResult<CategoryDto>> Create(CreateCategoryInput input)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(input.CreatedById);
                // AutoMapper ile Category sinifini CreateCategoryInput ile olusturabiliriz.
                // Category catMapper = AutoMapper.Mapper.Map<Category>(input);
                // Category catMapper = Mapper.Map<Category>(input);
                Category category = new Category
                {
                    Name = input.Name,
                    UrlName = input.UrlName,
                    CreatedDate = DateTime.Now,
                    CreatedById = input.CreatedById,
                    CreatedBy = user.UserName,
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>();
                // haftaya bu kod yokalacak yerine automapper metodu gelecek!
                // AutoMapper ile Category sinifini CategoryDto sinifina donusturebiliriz.
                result.Result = new CategoryDto
                {
                    CreatedById = category.CreatedById,
                    CreatedDate = category.CreatedDate,
                    CreatedBy = category.CreatedBy,
                    Id = category.Id,
                    Name = category.Name,
                    UrlName = category.UrlName
                };
                result.Succeeded = true;
                return result;
            }
            catch (Exception e)
            {
                ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>();
                result.Succeeded = false;
                // TODO: hata mesajini da gonder
                return result;
            }


        }

        public Task<ApplicationResult> Delete(int Id)
        {
            throw new NotImplementedException();
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
            catch (Exception e)
            {
                return new ApplicationResult<CategoryDto>
                {
                    Result = new CategoryDto(),
                    Succeeded = false
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
            catch (Exception e)
            {
                return new ApplicationResult<List<CategoryDto>>
                {
                    Result = new List<CategoryDto>(),
                    Succeeded = false
                };
            }
        }

        public Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryInput input)
        {
            throw new NotImplementedException();
        }
    }
}
