using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StartEFCore.Controllers
{
    public class TeamController : Controller
    {
        // TODO: TAKIMLARI LİSTELEMEK (list)
        public IActionResult Index()
        {
            return View();
        }

        // TODO: Yeni Takım Oluşturmak (create)

        // TODO: Id'si eşit olan takımın bilgileri (detail)

        // TODO: Id'si eşit olan takımın bilgilerini güncelle (update)
        
        // TODO: Id'si eşit olan takımı sil (delete)
    }
}