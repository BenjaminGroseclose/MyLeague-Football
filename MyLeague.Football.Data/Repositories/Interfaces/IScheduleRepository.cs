using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        IEnumerable<ScheduleWeek> GetScheduleBySeason(int season);

        void SaveScore(int id, int awayTeamScore, int homeTeamScore);

        void SaveSchedule(IEnumerable<ScheduleWeek> schedules);
    }
}
