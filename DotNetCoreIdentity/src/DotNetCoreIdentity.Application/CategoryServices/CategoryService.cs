using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;

namespace DotNetCoreIdentity.Application
{
    public class CategoryService : ICategoryService
    {
        public Task<ApplicationResult<CategoryDto>> Create(CreateCategoryInput input)
        {
            throw new NotImplementedException();
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
