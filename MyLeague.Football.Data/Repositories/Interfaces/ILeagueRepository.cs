using MyLeague.Football.Data.Models;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        League GetLeague(int id);
        void CreateLeague(League league);
        void UpdateLeague(League league);
    }
}
