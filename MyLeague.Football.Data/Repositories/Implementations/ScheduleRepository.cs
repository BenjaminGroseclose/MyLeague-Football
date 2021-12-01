using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.Data.Repositories.Implementations
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly MyLeagueFootballContext dbContext;

        public ScheduleRepository(MyLeagueFootballContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ScheduleWeek> GetScheduleBySeason(int season)
        {
            return this.dbContext.ScheduleWeeks.Include(x => x.HomeTeam).Include(x => x.AwayTeam).Where(x => x.Season == season);
        }

        public void SaveSchedule(IEnumerable<ScheduleWeek> schedules)
        {
            this.dbContext.AddRange(schedules);
            var rows = this.dbContext.SaveChanges();

            if (rows != schedules.Count())
            {
                throw new ArgumentOutOfRangeException($"Saved incorrect number of rows. Expected: {schedules.Count()} Actual: {rows}");
            }

            return;
        }

        public void SaveScore(int id, int awayTeamScore, int homeTeamScore)
        {
            throw new NotImplementedException();
        }
    }
}
