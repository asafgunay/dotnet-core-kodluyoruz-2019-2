using Microsoft.AspNetCore.Mvc;
using StartEFCore.Entityframework;
using StartEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.ViewComponents
{
    [ViewComponent]
    public class TeamInfoViewComponent : ViewComponent
    {
        private readonly StartEFCoreDbContext _context;
        public TeamInfoViewComponent(StartEFCoreDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int teamId)
        {
            Team team = _context.Teams.Find(teamId);
            return View(team);
        }

    }
}
