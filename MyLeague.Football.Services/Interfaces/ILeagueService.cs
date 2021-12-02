using MyLeague.Football.Data.Models;
using System.Threading.Tasks;

namespace MyLeague.Football.Services.Interfaces
{
    public interface ILeagueService
    {
        Task CreateLeague(string coachFirstName, string coachLastName, Franchise franchise);

        // TODO:
        // IEnumerable<ScheduleWeek> GenerateSchedule(IEnumerable<Franchise> franchises, int season);
    }
}
