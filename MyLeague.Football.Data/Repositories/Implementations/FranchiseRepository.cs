using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Core;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.Data.Repositories.Implementations
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly MyLeagueFootballContext dbContext;

        public FranchiseRepository(MyLeagueFootballContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Franchise> GetAll(bool removeByeWeek = true)
        {
            if (removeByeWeek)
            {
                return this.dbContext.Franchises.Where(x => !Constants.BYE_ABBREVATION.Equals(x.Abbrevation)).Include(x => x.DraftPicks).ToList();
            }
            else
            {
                return this.dbContext.Franchises.Include(x => x.DraftPicks).ToList();
            }
        }

        public Franchise GetById(int id)
        {
            var franchise = this.dbContext.Franchises.Find(id);

            if (franchise == null)
            {
                throw new ArgumentException($"Unable to find a franchise with id: {id}");
            }

            return franchise;
        }

        public Franchise SetAsUser(int id)
        {
            Franchise franchiseToUpdate = this.dbContext.Franchises.Find(id);

            if (franchiseToUpdate == null)
            {
                throw new ArgumentNullException($"No franchise with id: {id} found");
            }

            franchiseToUpdate.SetAsPlayer();

            var rows = this.dbContext.SaveChanges();

            if (rows != 1)
            {
                throw new Exception($"Unexpected number of rows ({rows}) update");
            }

            return franchiseToUpdate;
        }

        public void UpdateDraftPicks(int franchiseId, DraftPick draftPick)
        {
            var pick = this.dbContext.DraftPicks.Find(draftPick.Id);

            if (pick == null)
            {
                throw new ArgumentException($"Was not able to find a drat pick with id: {draftPick.Id}");
            }

            pick.OwnerId = franchiseId;

            this.dbContext.SaveChanges();
        }
    }
}