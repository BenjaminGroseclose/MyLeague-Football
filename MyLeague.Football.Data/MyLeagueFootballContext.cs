using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Generators;
using MyLeague.Football.Data.Models;
using System.Linq;
using static MyLeague.Football.Data.Generators.PlayerGenerator;

namespace MyLeague.Football.Data
{
    public class MyLeagueFootballContext : DbContext
    {
        public MyLeagueFootballContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var test = FranchiseGenerator.CreateDefaultFranchises();
            modelBuilder.Entity<Franchise>()
                .HasData(test);

            modelBuilder.Entity<Franchise>()
                .HasMany(x => x.Players);

            modelBuilder.Entity<League>()
                .HasOne(x => x.ChoosenFranchise);

            PlayerGeneratorValues playerData = CreateDefaultPlayer();

            if (playerData.Players.Any(x => x.PlayerAttributeId == default(int)))
            {
                throw new System.Exception();
            }

            modelBuilder.Entity<PlayerAttributes>()
                .HasData(playerData.Attributes);

            modelBuilder.Entity<Player>()
                .HasData(playerData.Players);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PlayerAttributes> PlayerAttributes { get; set; }
        public DbSet<ScheduleWeek> ScheduleWeeks { get; set; }
    }
}
