using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Common;
using CommunityToolkit.WinUI;
using CommunityToolkit.Mvvm.ComponentModel;
using MyLeague.Football.Data.Repository.Interfaces;
using MyLeague.Football.Data.Models;

namespace MyLeague.Football.ViewModels
{
    public class CreateLeagueViewModel : ObservableRecipient
    {
        private readonly IFranchiseRepository franchiseRepository;
        public CreateLeagueViewModel(IFranchiseRepository franchiseRepository)
        {
            this.franchiseRepository = franchiseRepository;

            this.Franchises = this.franchiseRepository.GetAll(true);
        }

        public IEnumerable<Franchise> Franchises { get; set; }
    }
}
