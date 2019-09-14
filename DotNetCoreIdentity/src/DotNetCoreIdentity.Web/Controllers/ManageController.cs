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
    [Authorize(Roles = "Admin")]
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

        [Route("Users/Detail/{userId}")]
        public async Task<IActionResult> UserDetail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                FullName = user.FirstName + " " + user.LastName,
                NationalIdNumber = user.NationalIdNumber
            };
            ViewBag.UserRoles = "Role sahip degil";
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                ViewBag.UserRoles = string.Join(", ", userRoles);
            }
            return View(model);
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
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var assignRole = await _userManager.AddToRoleAsync(user, role.Name);
                if (assignRole.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kayıt esnasında bir hata oluştu!");
                    var roleAssignationErrors = assignRole.Errors.Select(x => x.Description);
                    ModelState.AddModelError(string.Empty,
                        string.Join(", ", roleAssignationErrors));
                }
            }
            return View(model);
        }
        [Route("Roles/Revoke/{userId}")]
        public async Task<IActionResult> RevokeRole(string userId)
        {
            AssignRoleViewModel model = new AssignRoleViewModel();
            model.UserId = userId;

            var user = await _userManager.FindByIdAsync(userId);
            var userRolesStrList = await _userManager.GetRolesAsync(user);
            if (userRolesStrList.Any())
            {
                var userRoles = _roleManager.Roles.Where(x => userRolesStrList.Contains(x.Name)).ToList();
                model.RoleList = userRoles.Select(x => new SelectListItem
                {
                    Selected = false,
                    Text = x.Name,
                    Value = x.Id
                }).ToList();
            }
            else
            {
                model.RoleList = new List<SelectListItem>();
            }

            return View(model);
        }
        [HttpPost]
        [Route("Roles/Revoke/{userId}")]
        public async Task<IActionResult> RevokeRole(AssignRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new { userId = model.UserId });
                }
                ModelState.AddModelError(string.Empty, "Bir hata oluştu, lütfen tekrar deneyiniz!");
            }
            return View(model);
        }
    }
}