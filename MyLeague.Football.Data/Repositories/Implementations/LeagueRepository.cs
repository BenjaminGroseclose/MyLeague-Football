using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace MyLeague.Football.Data.Repositories.Implementations
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly MyLeagueFootballContext dbContext;

        public LeagueRepository(MyLeagueFootballContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateLeague(League league)
        {
            this.dbContext.Leagues.Add(league);
            this.dbContext.SaveChanges();
        }

        public League GetLeague(int id)
        {
            League league = this.dbContext.Leagues.Include(x => x.ChoosenFranchise).ThenInclude(x => x.DraftPicks).FirstOrDefault(x => x.Id == id);

            if (league == null)
            {
                throw new Exception($"Unable to find league with id: {id}");
            }

            return league;
        }

        public void UpdateLeague(League league)
        {
            var leagueToUpdate = this.dbContext.Leagues.Find(1);

            if (leagueToUpdate != null)
            {
                leagueToUpdate.ChoosenFranchise = league.ChoosenFranchise;
                leagueToUpdate.LeagueDate = league.LeagueDate;

                this.dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Was not able to find a league with PK of 1");
            }
        }
    }
}
