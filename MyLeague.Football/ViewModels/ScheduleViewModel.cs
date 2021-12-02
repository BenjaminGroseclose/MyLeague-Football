using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MyLeague.Football.Core;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.ViewModels
{
    public class ScheduleViewModel : ObservableRecipient
    {
        private readonly ILeagueRepository leagueRepository = Ioc.Default.GetService<ILeagueRepository>();
        private readonly IScheduleRepository scheduleRepository = Ioc.Default.GetService<IScheduleRepository>();
        private readonly IEnumerable<ScheduleWeek> fullSchedule;

        public ScheduleViewModel()
        {
            var league = this.leagueRepository.GetLeague(1);
            this.fullSchedule = this.scheduleRepository.GetScheduleBySeason(league.CurrentSeason);

            this.ScheduleThisWeek = this.fullSchedule.Where(x => x.Week == league.CurrentWeek && !Constants.BYE_ABBREVATION.Equals(x.AwayTeam.Abbrevation))
                                                     .OrderBy(x => x.DateOfGame);

            this.AllWeeks = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
            this.SelectedWeek = league.CurrentWeek.ToString();
        }

        private List<string> allWeeks;

        public List<string> AllWeeks
        {
            get => this.allWeeks;
            set => SetProperty(ref this.allWeeks, value);
        }

        private IEnumerable<ScheduleWeek> scheduleThisWeek;
        public IEnumerable<ScheduleWeek> ScheduleThisWeek
        {
            get => scheduleThisWeek;
            set => SetProperty(ref scheduleThisWeek, value);
        }

        private string selectedWeek;

        public string SelectedWeek
        {
            get => selectedWeek;
            set
            {
                SetProperty(ref selectedWeek, value);
                this.ScheduleThisWeek = this.fullSchedule.Where(x => x.Week == int.Parse(value) && !Constants.BYE_ABBREVATION.Equals(x.AwayTeam.Abbrevation))
                                                         .OrderBy(x => x.DateOfGame);
            }
        }
    }
}
