using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data
{
    public class MyLeagueFootballContext : DbContext
    {
        public MyLeagueFootballContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PlayerAttributes> PlayerAttributes { get; set; }
    }
}
