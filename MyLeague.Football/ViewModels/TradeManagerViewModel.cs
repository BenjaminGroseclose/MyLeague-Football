using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Interfaces;
using MyLeague.Football.Services.Requests;
using MyLeague.Football.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MyLeague.Football.ViewModels
{
    /// <summary>
    /// TODO: Figure out how to remove players / pick from the list after you add them to the trade.
    /// </summary>
    public class TradeManagerViewModel : ObservableRecipient
    {
        private readonly ITradeService tradeService = Ioc.Default.GetService<ITradeService>();
        private readonly ILeagueRepository leagueRepository = Ioc.Default.GetService<ILeagueRepository>();
        private readonly IFranchiseRepository franchiseRepository = Ioc.Default.GetService<IFranchiseRepository>();
        private readonly IPlayerRepository playerRepository = Ioc.Default.GetService<IPlayerRepository>();
        private IEnumerable<Player> allPlayers;

        public TradeManagerViewModel()
        {
            var league = this.leagueRepository.GetLeague(1);

            this.UserFranchise = league.ChoosenFranchise;
            this.ComputerFranchises = this.franchiseRepository.GetAll().Where(x => x.IsComputer).OrderBy(x => x.FullName);
            this.allPlayers = this.playerRepository.GetAllPlayers();

            this.UserPlayers = this.allPlayers.Where(x => x.FranchiseId == league.ChoosenFranchise.Id).ToList();

            this.SubmitTradeCommand = new RelayCommand(SubmitTrade);
            this.Positions = Enum.GetValues(typeof(Position)).Cast<Position>().ToList();

            this.UserOffer = new TradeOffer(this.UserFranchise, new List<Player>(), new List<DraftPick>());
            this.ComputerOffer = new TradeOffer(null, new List<Player>(), new List<DraftPick>());

            this.SelectedComputerFranchise = this.ComputerFranchises.First();
        }

        private string tradeResultMessage;

        public string TradeResultMessage
        {
            get => tradeResultMessage;
            set => SetProperty(ref tradeResultMessage, value);
        }

        public ICommand SubmitTradeCommand { get; }

        public List<Position> Positions { get; set; }

        private Franchise userFranchise;

        public Franchise UserFranchise
        {
            get => userFranchise;
            set => SetProperty(ref userFranchise, value);
        }

        private TradeOffer userOffer;

        public TradeOffer UserOffer
        {
            get => userOffer;
            set => SetProperty(ref userOffer, value);
        }

        private List<Player> userPlayers;

        public List<Player> UserPlayers
        {
            get => userPlayers;
            set => SetProperty(ref userPlayers, value);
        }

        public DraftPick SelectedUserDraftPick
        {
            set
            {
                this.UserOffer = new TradeOffer(this.UserFranchise,
                                                this.UserOffer.Players,
                                                this.UserOffer.DraftPicks.Append(value).ToList());
            }
        }

        public Player SelectedUserPlayer
        {
            set
            {
                this.UserOffer = new TradeOffer(this.UserFranchise, 
                                                this.UserOffer.Players.Append(value).ToList(),
                                                this.UserOffer.DraftPicks);
            }
        }

        public DraftPick SelectedComputerDraftPick
        {
            set
            {
                this.ComputerOffer = new TradeOffer(this.SelectedComputerFranchise,
                                                this.ComputerOffer.Players,
                                                this.ComputerOffer.DraftPicks.Append(value).ToList());
            }
        }

        public Player SelectedComputerPlayer
        {
            set
            {
                this.ComputerOffer = new TradeOffer(this.SelectedComputerFranchise,
                                                this.ComputerOffer.Players.Append(value).ToList(),
                                                this.ComputerOffer.DraftPicks);
            }
        }

        private Position selectedUserPosition;

        public Position SelectedUserPosition
        {
            get => selectedUserPosition;
            set
            {
                SetProperty(ref selectedUserPosition, value);

                if (value == Position.ALL)
                {
                    this.UserPlayers = this.allPlayers.Where(x => x.FranchiseId == this.UserFranchise.Id).ToList();
                }
                else
                {
                    this.UserPlayers = this.allPlayers.Where(x => x.FranchiseId == this.UserFranchise.Id && x.Position == value).ToList();
                }
            }
        }

        private Franchise selectedComputerFranchise;

        public Franchise SelectedComputerFranchise
        {
            get => selectedComputerFranchise;
            set
            {
                SetProperty(ref selectedComputerFranchise, value);
                this.ComputerOffer = new TradeOffer(value, new List<Player>(), new List<DraftPick>());
                this.ComputerPlayers = this.allPlayers.Where(x => x.FranchiseId == value.Id).ToList();
            }
        }

        private List<Player> computerPlayers;

        public List<Player> ComputerPlayers
        {
            get => computerPlayers;
            set => SetProperty(ref computerPlayers, value);
        }

        private TradeOffer computerOffer;

        public TradeOffer ComputerOffer
        {
            get => computerOffer;
            set => SetProperty(ref computerOffer, value);
        }

        private Position selectedComputerPosition;

        public Position SelectedComputerPosition
        {
            get => selectedComputerPosition;
            set
            {
                SetProperty(ref selectedComputerPosition, value);

                if (value == Position.ALL)
                {
                    this.ComputerPlayers = this.allPlayers.Where(x => x.FranchiseId == this.SelectedComputerFranchise.Id).ToList();
                }
                else
                {
                    this.ComputerPlayers = this.allPlayers.Where(x => x.FranchiseId == this.SelectedComputerFranchise.Id && x.Position == value).ToList();
                }
            }
        }

        private IEnumerable<Franchise> computerFranchises;
        public IEnumerable<Franchise> ComputerFranchises
        {
            get => computerFranchises;
            set => SetProperty(ref computerFranchises, value);
        }

        private void SubmitTrade()
        {
            if (this.UserOffer.Players.Any() == false && this.UserOffer.DraftPicks.Any() == false)
            {
                // TODO: Popup to tell them they need to provide a valid user offer;
            }

            if (this.ComputerOffer.Franchise == null || (this.ComputerOffer.Players.Any() == false && this.ComputerOffer.DraftPicks.Any()))
            {
                // TODO: Popup to tell them they need to provide a valid computer offer;
            }

            TradeResult tradeResult = this.tradeService.ProposeTrade(this.UserOffer, this.ComputerOffer);
            this.TradeResultMessage = $"{tradeResult.Reason} between {this.UserFranchise.FullName} and {this.SelectedComputerFranchise.FullName}";
            this.UserOffer = new TradeOffer(this.UserFranchise, new List<Player>(), new List<DraftPick>());
            this.ComputerOffer = new TradeOffer(this.SelectedComputerFranchise, new List<Player>(), new List<DraftPick>());
        }
    }
}
