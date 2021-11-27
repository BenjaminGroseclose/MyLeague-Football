using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        void CreateLeague(League league);
        void UpdateLeague(League league);
    }
}
