using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using MyLeague.Football.Data.Repository.Interfaces;
using MyLeague.Football.Data.Models;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace MyLeague.Football.ViewModels
{
    public class CreateLeagueViewModel : ObservableRecipient
    {
        private readonly IFranchiseRepository franchiseRepository = Ioc.Default.GetService<IFranchiseRepository>();
        public CreateLeagueViewModel()
        {
            this.Franchises = this.franchiseRepository.GetAll(true);
        }

        public IEnumerable<Franchise> Franchises { get; set; }
    }
}
