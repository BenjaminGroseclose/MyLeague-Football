using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
            this.Franchises = this.franchiseRepository.GetAll().OrderBy(x => x.FullName);
            this.ShowErrors = Visibility.Collapsed;
            this.CreateLeaugeCommand = new AsyncRelayCommand(CreateLeague);
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

        private async Task CreateLeague()
        {
            if (!this.IsValidSubmit())
            {
                this.ShowErrors = Visibility.Visible;
                return;
            }

            this.ShowErrors = Visibility.Collapsed;

            await this.leagueService.CreateLeague(this.CoachFirstName, this.CoachLastName, this.SelectedFranchise);

            Application.Current.MainWindow = new GameWindow();
            Application.Current.MainWindow.Show();

            Window window = Application.Current.Windows[0];

            if (window != null)
            {
                window.Close();
            }
        }

        private bool IsValidSubmit()
        {
            return this.SelectedFranchise != null &&
                   !string.IsNullOrEmpty(this.CoachFirstName) &&
                   !string.IsNullOrEmpty(this.CoachLastName);
        }
    }
}
