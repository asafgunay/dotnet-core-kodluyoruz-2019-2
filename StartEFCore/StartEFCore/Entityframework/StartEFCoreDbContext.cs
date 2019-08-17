using Microsoft.EntityFrameworkCore;
using StartEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.Entityframework
{
    public class StartEFCoreDbContext : DbContext
    {
        // yapici metod
        public StartEFCoreDbContext(DbContextOptions<StartEFCoreDbContext> options) : base(options)
        {
            // bos constructor
        }

        // model siniflari al ve ef ile tanistir yani contexte ekle
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

    }
}
