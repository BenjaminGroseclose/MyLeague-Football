using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.ViewModels
{
    public class GameWindowViewModel : ObservableRecipient
    {
        private readonly ILeagueRepository leagueRepository = Ioc.Default.GetService<ILeagueRepository>();
        private readonly IScheduleRepository scheduleRepository = Ioc.Default.GetService<IScheduleRepository>();

        public GameWindowViewModel()
        {
            var league = this.leagueRepository.GetLeague(1);
            this.CoachName = $"{league.CoachFirstName} {league.CoachLastName}";
            this.Logo = league.ChoosenFranchise.Logo;
            this.FranchiseFullName = league.ChoosenFranchise.FullName;

            var schedule = this.scheduleRepository.GetScheduleBySeason(league.CurrentSeason);

            IEnumerable<ScheduleWeek> currentWeek = schedule.Where(x => x.Week == league.CurrentWeek);

            var ourGame = currentWeek.First(x => x.HomeTeam.Id == league.ChoosenFranchise.Id || x.AwayTeam.Id == league.ChoosenFranchise.Id);

            if (ourGame.HomeTeam.Id == league.ChoosenFranchise.Id)
            {
                this.OpponentLogo = ourGame.AwayTeam.Logo;
                this.OpponentFullName = ourGame.AwayTeam.FullName;
            }
            else
            {
                this.OpponentLogo = ourGame.HomeTeam.Logo;
                this.OpponentFullName = ourGame.HomeTeam.FullName;
            }

        }

        private string coachName;

        public string CoachName
        {
            get => coachName;
            set => SetProperty(ref coachName, value);
        }

        private string logo;

        public string Logo
        {
            get => logo;
            set => SetProperty(ref logo, value);
        }

        private string franchiseFullName;

        public string FranchiseFullName
        {
            get => franchiseFullName;
            set => SetProperty(ref franchiseFullName, value);
        }

        private string opponentLogo;

        public string OpponentLogo
        {
            get => opponentLogo;
            set => SetProperty(ref opponentLogo, value);
        }

        private string opponentFullName;

        public string OpponentFullName
        {
            get => opponentFullName;
            set => SetProperty(ref opponentFullName, value);
        }
    }
}
