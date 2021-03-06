using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers();

        IEnumerable<Player> GetPlayersByFranchise(int franchiseId);

        Player GetPlayerById(int playerId);

        Player UpdatePlayer(int id, Player player);
    }
}
