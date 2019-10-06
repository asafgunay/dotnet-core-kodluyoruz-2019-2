using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreIdentity.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/Category
        [HttpGet]
        public async Task<ApplicationResult<List<CategoryDto>>> Get()
        {
            return await _categoryService.GetAll();
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ApplicationResult<CategoryDto>>> Get(int id)
        {
            var result = await _categoryService.Get(id);
            if (result.Succeeded)
                return result;
            return NotFound(result);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<ApplicationResult<CategoryDto>>> Post([FromBody] CreateCategoryInput input)
        {
            var result = await _categoryService.Create(input);
            if (result.Succeeded)
                return result;
            return NotFound(result);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
