using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreIdentity.Web.Controllers
{
    public class ManageController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        public IActionResult EditRole()
        {
            return View();
        }
        public IActionResult DeleteRole()
        {
            return View();
        }
        public IActionResult AssignRole(string userId, string roleId)
        {
            return View();
        }
        public IActionResult RevokeRole(string userId, string roleId)
        {
            return View();
        }

    }
}