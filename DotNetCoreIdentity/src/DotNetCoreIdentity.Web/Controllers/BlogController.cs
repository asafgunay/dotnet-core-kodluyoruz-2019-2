using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.BlogServices;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreIdentity.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        public BlogController(IPostService postService)
        {
            _postService = postService;
        }
        [Route("{categoryUrl}/{postUrl}")]
        public async Task<IActionResult> Index(string categoryUrl, string postUrl)
        {
            ApplicationResult<PostDto> getPost = await _postService.GetByUrl(categoryUrl, postUrl);
            if (getPost.Succeeded)
            {
                return View(getPost.Result);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}