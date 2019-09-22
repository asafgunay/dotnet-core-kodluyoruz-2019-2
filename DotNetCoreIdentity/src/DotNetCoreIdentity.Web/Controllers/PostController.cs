using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.BlogServices;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetCoreIdentity.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public PostController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }
        // liste
        public async Task<IActionResult> Index()
        {
            var getAllService = await _postService.GetAll();
            List<PostDto> model = getAllService.Result;
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected= false,
                Text=x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInput model)
        {
            return View(model);
        }
    }
}