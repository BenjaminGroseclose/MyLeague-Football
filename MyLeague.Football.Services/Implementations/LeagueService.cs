using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Data.Repository.Interfaces;
using MyLeague.Football.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Services.Implementations
{
    public class LeagueService : ILeagueService
    {
        private readonly IFranchiseRepository franchiseRepository;
        private readonly ILeagueRepository leagueRepository;

        public LeagueService(IFranchiseRepository franchiseRepository, ILeagueRepository leagueRepository)
        {
            this.franchiseRepository = franchiseRepository;   
            this.leagueRepository = leagueRepository;
        }

        public void CreateLeague(string coachFirstName, string coachLastName, Franchise franchise)
        {
            this.leagueRepository.CreateLeague(new League
            {
                Id = 1,
                ChoosenFranchise = franchise,
                LeagueDate = new DateTime(2021, 8, 16)
            });
        }
    }
}
