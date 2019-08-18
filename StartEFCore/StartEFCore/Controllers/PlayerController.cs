using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                return RedirectToAction("TeamPlayers", new { id = model.TeamId });
            }
            return View(model);
        }

        // TODO: Id'si eşit olan oyuncunun bilgileri (detail)
        public IActionResult Details(int id)
        {
            Player model = _context.Players.Find(id);
            return View(model);
        }

        // TODO: Id'si eşit olan oyuncunun bilgilerini güncelle (update)
        public IActionResult Edit(int id)
        {
            Player model = _context.Players.Find(id);
            ViewBag.TeamsDDL = _context.Teams.Select(u => new SelectListItem
            {
                Selected = false,
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Player model)
        {
            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                // context ile guncelleme
                try
                {
                    TryToUpdatePlayer(model);
                    // return RedirectToAction("Edit", new {id = id });
                    return RedirectToAction("Edit", new { id });
                }
                catch (DBConcurrencyException ex)
                {
                    if (_context.Players.Find(id) == null)
                        return NotFound();
                    throw (ex);
                }

            }
            return View(model);
        }

        // void method hic birsey donmez
        private void TryToUpdatePlayer(Player model)
        {
            _context.Players.Update(model);
            _context.SaveChanges();
        }
        // TODO: Id'si eşit olan oyuncuyu sil (delete)
    }
}