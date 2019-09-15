using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Domain.Identity;
using DotNetCoreIdentity.Domain.PostTypes;
using DotNetCoreIdentity.EF.Context;
using Microsoft.AspNetCore.Identity;

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
            catch(Exception e)
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

        public Task<ApplicationResult<CategoryDto>> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<List<CategoryDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryInput input)
        {
            throw new NotImplementedException();
        }
    }
}
