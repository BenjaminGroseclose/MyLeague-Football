using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.ViewModels
{
    public class RosterManagementViewModel : ObservableRecipient
    {
        private readonly ILeagueRepository leagueRepository = Ioc.Default.GetService<ILeagueRepository>();
        private readonly IFranchiseRepository franchiseRepository = Ioc.Default.GetService<IFranchiseRepository>();
        private readonly IPlayerRepository playerRepository = Ioc.Default.GetService<IPlayerRepository>();
        private readonly IEnumerable<Player> players;
        public RosterManagementViewModel()
        {
            var league = this.leagueRepository.GetLeague(1);

            this.players = this.playerRepository.GetAllPlayers();

            this.Franchises = this.franchiseRepository.GetAll().OrderBy(x => x.FullName);
            this.SelectedFranchise = league.ChoosenFranchise;
            this.Positions = Enum.GetValues(typeof(Position)).Cast<Position>().ToList();
            this.SelectedPosition = Position.ALL;
        }

        public List<Position> Positions { get; set; }

        private Position selectedPosition;

        public Position SelectedPosition
        {
            get => selectedPosition;
            set
            {
                SetProperty(ref selectedPosition, value);

                if (value == Position.ALL)
                {
                    this.DisplayPlayers = this.players.Where(x => x.FranchiseId == this.SelectedFranchise.Id);
                }
                else
                {
                    this.DisplayPlayers = this.players.Where(x => x.FranchiseId == this.SelectedFranchise.Id && x.Position == value);
                }
            }
        }

        private IEnumerable<Player> displayPlayers;

        public IEnumerable<Player> DisplayPlayers
        {
            get => displayPlayers;
            set => SetProperty(ref displayPlayers, value);
        }

        private IEnumerable<Franchise> franchises;

        public IEnumerable<Franchise> Franchises
        {
            get => franchises;
            set => SetProperty(ref franchises, value);
        }

        private Franchise selectedFranchise;

        public Franchise SelectedFranchise
        {
            get => selectedFranchise;
            set
            {
                SetProperty(ref selectedFranchise, value);
                this.DisplayPlayers = this.players.Where(x => x.FranchiseId == value.Id);
                this.SelectedPosition = Position.ALL;
            }
        }
    }
}
