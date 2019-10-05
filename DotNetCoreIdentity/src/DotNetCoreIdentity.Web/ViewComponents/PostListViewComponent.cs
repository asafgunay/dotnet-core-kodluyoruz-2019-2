using DotNetCoreIdentity.Application.BlogServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Web.ViewComponents
{
    [ViewComponent]
    public class PostListViewComponent : ViewComponent
    {
        private readonly IPostService _postService;
        public PostListViewComponent(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var postList = await _postService.GetAll();
           
            return View(postList.Result);
        }
     
    }
}
