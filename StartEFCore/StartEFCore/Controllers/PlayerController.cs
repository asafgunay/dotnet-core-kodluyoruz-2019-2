using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StartEFCore.Controllers
{
    public class PlayerController : Controller
    {
        // TeamId değerine eşit gelecek id parametresi alır
        // TODO: Takımın Oyuncularını Listele (list)
        public IActionResult Index(int id)
        {
            return View();
        }

        // TODO: Yeni Oyuncu Oluşturmak belirtilen takım için (create)

        // TODO: Id'si eşit olan oyuncunun bilgileri (detail)

        // TODO: Id'si eşit olan oyuncunun bilgilerini güncelle (update)

        // TODO: Id'si eşit olan oyuncuyu sil (delete)
    }
}