using MaterialDesignThemes.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.ViewModels
{
    public class TeamScheduleViewModel : ObservableRecipient
    {
        private readonly ILeagueRepository leagueRepository = Ioc.Default.GetService<ILeagueRepository>();
        private readonly IScheduleRepository scheduleRepository = Ioc.Default.GetService<IScheduleRepository>();
        private readonly IFranchiseRepository franchiseRepository = Ioc.Default.GetService<IFranchiseRepository>();
        private readonly IEnumerable<ScheduleWeek> fullSchedule;

        public TeamScheduleViewModel()
        {
            var league = this.leagueRepository.GetLeague(1);
            this.fullSchedule = this.scheduleRepository.GetScheduleBySeason(league.CurrentSeason);

            this.FranchiseSchedule = this.fullSchedule.Where(x => x.HomeTeam.Id == league.ChoosenFranchise.Id || x.AwayTeam.Id == league.ChoosenFranchise.Id )
                                                      .OrderBy(x => x.Week);

            this.Franchises = this.franchiseRepository.GetAll(false, true);
            this.SelectedFranchise = league.ChoosenFranchise;
        }

        private IEnumerable<Franchise> franchises;

        public IEnumerable<Franchise> Franchises
        {
            get => franchises;
            set => SetProperty(ref franchises, value);
        }

        private IEnumerable<ScheduleWeek> franchisesSchedule;
        public IEnumerable<ScheduleWeek> FranchiseSchedule
        {
            get => franchisesSchedule;
            set => SetProperty(ref franchisesSchedule, value);
        }

        private Franchise selectedFranchise;

        public Franchise SelectedFranchise
        {
            get => selectedFranchise;
            set
            {
                SetProperty(ref selectedFranchise, value);
                this.FranchiseSchedule = this.fullSchedule.Where(x => x.HomeTeam.Id == value.Id || x.AwayTeam.Id == value.Id).OrderBy(x => x.Week);
            }
        }
    }
}
