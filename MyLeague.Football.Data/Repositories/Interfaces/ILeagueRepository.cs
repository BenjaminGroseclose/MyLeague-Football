using MyLeague.Football.Data.Models;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        League GetLeague(int id);
        League CreateLeague(League league);
        void UpdateLeague(int it, League league);
    }
}
