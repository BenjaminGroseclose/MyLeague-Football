using Microsoft.EntityFrameworkCore;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.Data.Repositories.Implementations
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MyLeagueFootballContext dbContext;

        public PlayerRepository(MyLeagueFootballContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return this.dbContext.Players.Include(x => x.PlayerAttributes).Include(x => x.Contract);
        }

        public IEnumerable<Player> GetPlayersByFranchise(int franchiseId)
        {
            return this.dbContext.Players.Where(x => x.Franchise != null && x.Franchise.Id == franchiseId)
                                         .Include(x => x.PlayerAttributes)
                                         .Include(x => x.Contract);
        }
    }
}
