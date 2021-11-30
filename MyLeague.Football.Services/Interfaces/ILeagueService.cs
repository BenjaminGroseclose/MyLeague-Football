using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
