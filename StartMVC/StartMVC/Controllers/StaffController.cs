using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartMVC.Models;

namespace StartMVC.Controllers
{
    public class StaffController : Controller
    {
        public static List<Staff> staffs;
        public IActionResult Index()
        {
            List<Staff> staffList = Staff.GetSampleData();
            if (staffs != null && staffs.Any())
            {
                staffList.AddRange(staffs);
            }
            return View(staffList);
        }
        public IActionResult Detail(int? id)
        {
            List<Staff> staffList = Staff.GetSampleData();
            if (staffs != null && staffs.Any())
            {
                staffList.AddRange(staffs);
            }
            Staff item = staffList.Where(x => x.Id == id).FirstOrDefault();
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Staff model)
        {
            if (ModelState.IsValid)
            {
                if (staffs == null)
                {
                    staffs = new List<Staff>();
                }
                staffs.Add(model);
                return RedirectToAction("Detail", new { id = model.Id });
            }
            return View(model);
        }
    }
}