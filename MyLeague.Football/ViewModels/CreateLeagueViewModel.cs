using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repository.Interfaces;
using MyLeague.Football.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyLeague.Football.ViewModels
{
    public class CreateLeagueViewModel : ObservableRecipient
    {
        private readonly IFranchiseRepository franchiseRepository = Ioc.Default.GetService<IFranchiseRepository>();
        private readonly ILeagueService leagueService = Ioc.Default.GetService<ILeagueService>();
        public CreateLeagueViewModel()
        {
            this.Franchises = this.franchiseRepository.GetAll(true);
            this.ShowErrors = Visibility.Collapsed;
            this.CreateLeaugeCommand = new RelayCommand(CreateLeague);
        }

        public ICommand CreateLeaugeCommand { get; }

        public IEnumerable<Franchise> Franchises { get; set; }

        private string coachFirstName;
        public string CoachFirstName
        {
            get => coachFirstName;
            set => SetProperty(ref coachFirstName, value);
        }

        private string coachLastName;
        public string CoachLastName
        {
            get => coachLastName;
            set => SetProperty(ref coachLastName, value);
        }

        private Franchise selectedFranchise;

        public Franchise SelectedFranchise
        {
            get => selectedFranchise;
            set => SetProperty(ref selectedFranchise, value);
        }

        private Visibility showErrors;
        public Visibility ShowErrors
        {
            get => showErrors;
            set => SetProperty(ref showErrors, value);
        }

        private void CreateLeague()
        {
            if (!this.IsValidSubmit())
            {
                this.ShowErrors = Visibility.Visible;
                return;
            }

            this.ShowErrors = Visibility.Collapsed;

            this.leagueService.CreateLeague(this.CoachFirstName,
                                            this.CoachLastName,
                                            this.SelectedFranchise);
        }

        private bool IsValidSubmit()
        {
            return this.SelectedFranchise != null &&
                   !string.IsNullOrEmpty(this.CoachFirstName) &&
                   !string.IsNullOrEmpty(this.CoachLastName);
        }
    }
}
