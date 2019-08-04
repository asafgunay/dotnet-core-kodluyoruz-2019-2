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
        public IActionResult Index()
        {
            List<Product> productList = Product.GetSampleData();
            return View(productList);
        }
        public IActionResult Detail(int id)
        {
            List<Product> productList = Product.GetSampleData();
            // ornek data icerisindeki parametreden gelen id'ye sahip urunu getir!
            // Where kosulu tum dataList icerisindeki itemlarin kosullandirilmasidir.
            Product item = productList.Where(x => x.Id == id).FirstOrDefault();
            return View(item);
        }
    }
}