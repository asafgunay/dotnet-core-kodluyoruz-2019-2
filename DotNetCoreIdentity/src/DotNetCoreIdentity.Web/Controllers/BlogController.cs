using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreIdentity.Web.Controllers
{
    public class BlogController : Controller
    {
        [Route("{categoryUrl}/{postUrl}")]
        public IActionResult Index(string categoryUrl, string postUrl)
        {
            return View();
        }
    }
}