using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var getAllService = await _categoryService.GetAll();
            return View(getAllService.Result);
        }
        public async Task<IActionResult> Details(int id)
        {
            var getService = await _categoryService.Get(id);
            return View(getService.Result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInput model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _categoryService.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var getService = await _categoryService.Get(id);
            return View(getService.Result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var deleteService = await _categoryService.Delete(id);
                if (deleteService.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bir hata olustu!");
                }
            }
            return View(model);
        }
    }
}