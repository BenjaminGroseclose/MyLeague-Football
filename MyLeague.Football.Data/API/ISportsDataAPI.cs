using MyLeague.Football.Data.API.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.API
{
    public interface ISportsDataAPI
    {
        [Get("/scores/json/Schedules/{season}?key=bb7b7a823aef491d886b061c8c0da655")]
        Task<IEnumerable<SportsDataSchedule>> GetScheduleBySeason(int season);
    }
}
