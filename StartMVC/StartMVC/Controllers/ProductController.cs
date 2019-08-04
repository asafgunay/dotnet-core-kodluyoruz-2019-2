using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartMVC.Models;

namespace StartMVC.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> products;
        public IActionResult Index()
        {
            List<Product> productList = Product.GetSampleData();
            if(products!=null && products.Any())
            {
                productList.AddRange(products);
            }
            return View(productList);
        }
        public IActionResult Detail(int id)
        {
            List<Product> productList = Product.GetSampleData();
            if (products != null && products.Any())
            {
                productList.AddRange(products);
            }
            // ornek data icerisindeki parametreden gelen id'ye sahip urunu getir!
            // Where kosulu tum dataList icerisindeki itemlarin kosullandirilmasidir.
            Product item = productList.Where(x => x.Id == id).FirstOrDefault();
            return View(item);
        }
        // urun kayit formunu getirecek
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                if (products == null)
                {
                    products = new List<Product>();
                }
                products.Add(model);
                return RedirectToAction("Detail", new { id = model.Id });
            }
            return View(model);
        }
    }
}