using Microsoft.AspNetCore.Mvc;
using StartEFCore.Data;
using StartEFCore.Entityframework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.ViewComponents
{
    [ViewComponent]
    public class TeamPlayersListViewComponent : ViewComponent
    {
        private StartEFCoreDbContext _context;
        public TeamPlayersListViewComponent(StartEFCoreDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int teamId)
        {
            List<Player> players = _context.Players.Where(x => x.TeamId == teamId).ToList();

            return View(players);
        }



    }
}
