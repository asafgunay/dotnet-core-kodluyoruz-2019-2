using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartEFCore.Entityframework;
using StartEFCore.Models;

namespace StartEFCore.Controllers
{
    public class TeamController : Controller
    {
        private readonly StartEFCoreDbContext _context;
        public TeamController(StartEFCoreDbContext context)
        {
            _context = context;
        }

        // TODO: TAKIMLARI LİSTELEMEK (list)
        public IActionResult Index()
        {
            List<Team> list = _context.Teams.ToList();
            return View(list);
        }

        // TODO: Yeni Takım Oluşturmak (create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Team model)
        {
            if (ModelState.IsValid)
            {
                _context.Teams.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // TODO: Id'si eşit olan takımın bilgileri (detail)
        public IActionResult Detail(int id)
        {
            Team model = _context.Teams.
                Where(x => x.Id == id)
                .Include(x => x.Players).
                FirstOrDefault();
            return View(model);
        }

        // TODO: Id'si eşit olan takımın bilgilerini güncelle (update)
        public IActionResult Edit(int id)
        {
            var model = _context.Teams.Find(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, Team model)
        {
            // id ile model id bir birine esit mi
            // model.Id'ye sahip bir veri hala var mi?
            if (id != model.Id)
            {
                return NotFound();
            }
            // model validasyonu dogruysa
            if (ModelState.IsValid)
            {
                // try
                try
                {
                    Team willUpdate = _context.Teams.Find(model.Id);
                    willUpdate.Name = model.Name;
                    willUpdate.Province = model.Province;
                    willUpdate.HiddenValue = model.HiddenValue;
                    willUpdate.LogoUrl = model.LogoUrl;
                    willUpdate.Year = model.Year;
                    willUpdate.ModifiedDate = DateTime.UtcNow;
                    //_context.Teams.Update(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (_context.Teams.Find(model.Id) == null)
                    {
                        return NotFound();

                    }
                    throw (ex);
                }
                // catch(DBConcurrencyException ex)
            }
            // model valid degilse
            return View(model);
        }

        // TODO: Id'si eşit olan takımı sil (delete)
        public IActionResult Delete(int id)
        {
            var model = _context.Teams.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Team model)
        {
            // id ile model id bir birine esit mi
            // model.Id'ye sahip bir veri hala var mi?
            if (id != model.Id)
            {
                return NotFound();
            }
            // model validasyonu dogruysa

            // try
            try
            {

                // Delete yapılırken takıma ait oyuncu var mı kontrol et
                // yoksa sil
                // varsa silme
                if (_context.Players.Where(x => x.TeamId == model.Id).Any())
                {
                    return NotFound();
                }
                //var willDeleteTeam = _context.Teams.Find(model.Id);
                _context.Teams.Remove(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_context.Teams.Find(model.Id) == null)
                {
                    return NotFound();
                }
                throw (ex);
            }
        }

    }
}