using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartEFCore.Entityframework;
using StartEFCore.Models;

namespace StartEFCore.Controllers
{
    public class PlayerController : Controller
    {
        private readonly StartEFCoreDbContext _context;
        public PlayerController(StartEFCoreDbContext context)
        {
            _context = context;
        }

        // TODO: TUM OYUNCULARI GETIRECEK Index Action i yap
        public IActionResult Index()
        {
            return View();
        }

        // TeamId değerine eşit gelecek id parametresi alır
        // TODO: Takımın Oyuncularını Listele (list)
        public IActionResult TeamPlayers(int id)
        {
            List<Player> list = _context.Players.Where(x => x.TeamId == id).ToList();
            ViewData["TeamId"] = id;
            return View(list);
        }

        // TODO: Yeni Oyuncu Oluşturmak belirtilen takım için (create)
        //[Route("create/team/player/{id}")]
        public IActionResult CreatePlayerToTeam(int teamId)
        {
            Player model = new Player();
            model.TeamId = teamId;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePlayerToTeam(Player model)
        {
            // model dogruysa
            if (ModelState.IsValid)
            {
                // modeli contexte ekle
                _context.Players.Add(model);
                // contextteki tum degisiklikleri kaydet
                _context.SaveChanges();
            }
            return View(model);
        }

        // TODO: Id'si eşit olan oyuncunun bilgileri (detail)

        // TODO: Id'si eşit olan oyuncunun bilgilerini güncelle (update)

        // TODO: Id'si eşit olan oyuncuyu sil (delete)
    }
}