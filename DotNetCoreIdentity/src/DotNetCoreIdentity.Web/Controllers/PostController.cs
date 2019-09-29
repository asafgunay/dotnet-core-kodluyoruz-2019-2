using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCoreIdentity.Application;
using DotNetCoreIdentity.Application.BlogServices;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace DotNetCoreIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _env;


        public PostController(IPostService postService, ICategoryService categoryService, IFileProvider fileProvider, IHostingEnvironment env)
        {
            _postService = postService;
            _categoryService = categoryService;
            _fileProvider = fileProvider;
            _env = env;
        }
        // liste
        public async Task<IActionResult> Index()
        {
            var getAllService = await _postService.GetAll();
            List<PostDto> model = getAllService.Result;
            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            ApplicationResult<PostDto> data = await _postService.Get(id);
            return View(data.Result);
        }
        public async Task<IActionResult> Create()
        {
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInput model)
        {
            if (ModelState.IsValid)
            {
                // createdById alanini doldur
                model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                // createService e gonder
                var createService = await _postService.Create(model);
                // hata yoksa Index e redirect et
                if (createService.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                // hata varsa hatayi ModelState e ekle
                ModelState.AddModelError(string.Empty, createService.ErrorMessage);
            }
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var post = await _postService.Get(id);
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(c => new SelectListItem
            {
                Selected = false,
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            UpdatePostInput model = new UpdatePostInput
            {
                Id = post.Result.Id,
                CategoryId = post.Result.CategoryId,
                Content = post.Result.Content,
                Title = post.Result.Title,
                UrlName = post.Result.UrlName,
                CreatedById = post.Result.CreatedById
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, UpdatePostInput model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    ModelState.AddModelError(string.Empty, "Forma müdahele etme!");
                }
                else
                {
                    model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var updatePost = await _postService.Update(model);
                    if (updatePost.Succeeded)
                    {
                        return RedirectToAction("Details", new { id = model.Id });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Bir hata olustu:\n" + updatePost.ErrorMessage);
                    }
                }
            }
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(c => new SelectListItem
            {
                Selected = false,
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _postService.Get(id);
            return View(model.Result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, PostDto input)
        {
            if (ModelState.IsValid && id == input.Id)
            {
                var delete = await _postService.Delete(id);
                if (delete.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Bir hata olustu");
            return View(input);
        }
        public async Task<IActionResult> UploadImage(Guid id)
        {
            ApplicationResult<PostDto> data = await _postService.Get(id);
            return View(data.Result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                // resmi al degiskene ata
                FileInfo fi = new FileInfo(file.FileName);
                // bir dosya adi belirle
                var newFileName = id.ToString() + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                // resmi belirtilen path'e yukle
                var webPath = _env.WebRootPath;
                var path = Path.Combine("", webPath + @"\images\" + newFileName);
                var pathToSave = @"/images/" + newFileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // yukleme tamamlandiktan sonra resmin yolunu db'ye yukle (imageUrl alanini olustur)
                var updateUrl = await _postService.UpdateImageUrl(id, pathToSave);
                if (updateUrl.Succeeded)
                {
                    return RedirectToAction("UploadImage", new { id });
                }

            }

            return RedirectToAction("Error", "Home");
        }

    }
}