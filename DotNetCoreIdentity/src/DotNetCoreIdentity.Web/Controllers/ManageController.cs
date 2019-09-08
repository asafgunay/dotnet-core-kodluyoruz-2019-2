using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreIdentity.Domain.Identity;
using DotNetCoreIdentity.Web.ViewModels.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetCoreIdentity.Web.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("Manage")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ManageController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<UserViewModel> users = _userManager.Users
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FullName = x.FirstName + " " + x.LastName,
                    NationalIdNumber = x.NationalIdNumber
                })
                .ToList();
            // tum kullanicilar listelensin
            return View(users);
        }
        [Route("Roles")]
        public IActionResult Roles()
        {
            // tum roller listelensin
            List<RoleViewModel> roles = _roleManager.Roles
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            // belki component yapabiliriz
            return View(roles);
        }
        [Route("Roles/Create")]
        public IActionResult CreateRole()
        {
            // yeni rol olusturma
            // rolesList component bunun view'inda yer alabilir
            return View();
        }
        [HttpPost]
        [Route("Roles/Create")]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            // model valid mi
            if (ModelState.IsValid)
            {
                // bu rol var mi test edelim
                var roles = await _roleManager.FindByNameAsync(model.Name);
                if (roles != null)
                {
                    ModelState.AddModelError(string.Empty, "Bu isimde bir rol mevcut!");
                    return View(model);
                }
                // yoksa olusturalim
                var result = await _roleManager.CreateAsync(new IdentityRole { Name = model.Name });
                // rol listesine yonlendirelim
                if (result.Succeeded)
                    return RedirectToAction("Roles");
                else
                {
                    ModelState.AddModelError(string.Empty, "Kayıt esnasında bir hata oluştu!");
                    var roleCreationErrors = result.Errors.Select(x => x.Description);
                    ModelState.AddModelError(string.Empty,
                        string.Join(", ", roleCreationErrors));
                }

            }
            return View(model);
        }
        [Route("Roles/Edit/{id}")]
        public IActionResult EditRole(string id)
        {
            return View();
        }
        [Route("Roles/Edit/{id}")]
        public IActionResult DeleteRole(string id)
        {
            return View();
        }
        [HttpGet]
        [Route("Roles/Assign/{userId}")]
        public IActionResult AssignRole(string userId)
        {
            List<SelectListItem> roleList = _roleManager.Roles
                .Select(x => new SelectListItem
            {
                    Value = x.Id,
                    Text = x.Name,
                    Selected = false

                }).ToList();
            AssignRoleViewModel model = new AssignRoleViewModel
            {
                UserId = userId,
                RoleList = roleList
            };

            return View(model);
        }
        [HttpPost]
        [Route("Roles/Assign/{userId}")]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            return View();
        }
        [Route("Roles/Revoke/{userId}/{roleId}")]
        public IActionResult RevokeRole(string userId, string roleId)
        {
            return View();
        }

    }
}