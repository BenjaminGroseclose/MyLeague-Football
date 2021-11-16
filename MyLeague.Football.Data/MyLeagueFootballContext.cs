using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLeague.Football.Data
{
    public class MyLeagueFootballContext : DbContext
    {
        public MyLeagueFootballContext(DbContextOptions options) : base(options)
        {
        }

        /*
        /// <summary>
        /// Automatically handles <see cref="BaseDataModel.Created"/> and <see cref="BaseDataModel.Updated"/>
        /// and then call base <see cref="SaveChanges"/>
        /// </summary>
        /// <returns>number of rows affected</returns>
        public override int SaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseDataModel && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseDataModel)entityEntry.Entity).Updated = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseDataModel)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        /// <summary>
        /// Automatically handles <see cref="BaseDataModel.Created"/> and <see cref="BaseDataModel.Updated"/>
        /// and then call base <see cref="SaveChangesAsync"/>
        /// </summary>
        /// <returns>number of rows affected</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseDataModel && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseDataModel)entityEntry.Entity).Updated = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseDataModel)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        */

        public DbSet<User> Users { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PlayerAttributes> PlayerAttributes { get; set; }
    }
}
