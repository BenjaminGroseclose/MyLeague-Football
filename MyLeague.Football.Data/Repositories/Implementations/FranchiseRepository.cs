using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.Repositories.Implementations
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly MyLeagueFootballContext context;

        public FranchiseRepository(MyLeagueFootballContext context)
        {
            this.context = context;
        }

        public IEnumerable<Franchise> GetAll(bool includePlayer)
        {
            if (includePlayer)
            {
                return this.context.Franchises.Include(x => x.Players).ToList();
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

        public Franchise Update(Franchise franchise)
        {
            throw new NotImplementedException();
        }
    }
}