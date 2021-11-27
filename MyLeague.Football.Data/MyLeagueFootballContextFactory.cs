using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data
{
    internal class MyLeagueFootballContextFactory : IDesignTimeDbContextFactory<MyLeagueFootballContext>
    {
        public MyLeagueFootballContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyLeagueFootballContext>();
            optionsBuilder.UseSqlite($@"Data Source=MyLeague.db");

            return new MyLeagueFootballContext(optionsBuilder.Options);
        }
    }
}
