using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository
{
    public class RPGContext : DbContext
    {
        public DbSet<HeroModel> Heroes { get; set; }
        public DbSet<GameModel> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlite("Data Source=..\\..\\..\\rpg.db");
        }
    }
}
