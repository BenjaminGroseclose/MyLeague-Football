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
        void CreateLeague(string coachFirstName, string coachLastName, Franchise franchise);
    }
}
