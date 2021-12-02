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
        private readonly MyLeagueFootballContext context;

        public FranchiseRepository(MyLeagueFootballContext context)
        {
            this.context = context;
        }

        public IEnumerable<Franchise> GetAll(bool removeByeWeek = true)
        {
            if (removeByeWeek)
            {
                return this.context.Franchises.Where(x => !Constants.BYE_ABBREVATION.Equals(x.Abbrevation)).ToList();
            }
            else
            {
                return this.context.Franchises.ToList();
            }
        }

        public Franchise GetById()
        {
            throw new NotImplementedException();
        }

        public Franchise SetAsPlayer(int id)
        {
            Franchise franchiseToUpdate = this.context.Franchises.Find(id);

            if (franchiseToUpdate == null)
            {
                throw new ArgumentNullException($"No franchise with id: {id} found");
            }

            franchiseToUpdate.SetAsPlayer();

            var rows = this.context.SaveChanges();

            if (rows != 1)
            {
                throw new Exception($"Unexpected number of rows ({rows}) update");
            }

            return franchiseToUpdate;
        }
    }
}