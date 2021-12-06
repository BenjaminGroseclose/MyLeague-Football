using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Interfaces;
using MyLeague.Football.Services.Requests;
using MyLeague.Football.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly ILeagueRepository leagueRepository;
        private readonly IFranchiseRepository franchiseRepository;
        private readonly IPlayerRepository playerRepository;

        public TradeService(ILeagueRepository leagueRepository, IFranchiseRepository franchiseRepository, IPlayerRepository playerRepository)
        {
            this.leagueRepository = leagueRepository;
            this.franchiseRepository = franchiseRepository;
            this.playerRepository = playerRepository;
        }

        /// <inheritdoc />
        public TradeResult ProposeTrade(TradeOffer userOffer, TradeOffer computerOffer)
        {
            if (userOffer == null)
            {
                throw new ArgumentNullException(nameof(userOffer));
            }

            if (computerOffer == null)
            {
                throw new ArgumentNullException(nameof(computerOffer));
            }

            // Determine userOffer level
            IEnumerable<TradeOfferLevel> userOfferLevels = this.DetermineTradeValue(userOffer);
            IEnumerable<TradeOfferLevel> computerOfferLevels = this.DetermineTradeValue(computerOffer);

            TradeResult tradeResult = CalculateTradeResult(userOfferLevels, computerOfferLevels);

            if (tradeResult.IsAccepted == true)
            {
                // Process Trade
                this.ProcessTrade(userOffer, computerOffer);
            }

            return tradeResult;
        }

        private void ProcessTrade(TradeOffer userOffer, TradeOffer computerOffer)
        {
            Franchise playerFranchise = userOffer.Franchise;
            Franchise computerFranchise = computerOffer.Franchise;

            foreach (var player in userOffer.Players)
            {
                player.FranchiseId = computerFranchise.Id;
                this.playerRepository.UpdatePlayer(player.Id, player);
            }

            foreach (var player in computerOffer.Players)
            {
                player.FranchiseId = playerFranchise.Id;
                this.playerRepository.UpdatePlayer(player.Id, player);
            }

            foreach (var pick in userOffer.DraftPicks)
            {
                this.franchiseRepository.UpdateDraftPicks(computerFranchise.Id, pick);
            }

            foreach (var pick in computerOffer.DraftPicks)
            {
                this.franchiseRepository.UpdateDraftPicks(playerFranchise.Id, pick);
            }
        }

        private IEnumerable<TradeOfferLevel> DetermineTradeValue(TradeOffer tradeOffer)
        {
            List<TradeOfferLevel> tradeOfferLevels = new List<TradeOfferLevel>();

            // Evaluate Player Value
            foreach (var player in tradeOffer.Players)
            {
                // Is player super star?
                if (player.PlayerAttributes.Overall >= 95)
                {
                    var league = this.leagueRepository.GetLeague(1);
                    if (player.Age(league.LeagueDate) <= 28)
                    {
                        // Young Super Star
                        tradeOfferLevels.Add(TradeOfferLevel.YOUNG_SUPER_STAR_PLAYER);
                        continue;
                    }
                    else
                    {
                        tradeOfferLevels.Add(TradeOfferLevel.OLDER_SUPER_STAR_PLAYER);
                        continue;
                    }
                }

                if (player.PlayerAttributes.Overall >= 90)
                {
                    tradeOfferLevels.Add(TradeOfferLevel.STAR_PLAYER);
                    continue;
                }

                if (player.PlayerAttributes.Overall >= 80)
                {
                    tradeOfferLevels.Add(TradeOfferLevel.AVERAGE_PLAYER);
                    continue;
                }

                if (player.PlayerAttributes.Overall < 80)
                {
                    tradeOfferLevels.Add(TradeOfferLevel.BAD_PLAYER);
                    continue;
                }
            }

            // Evaluate Draft Pick Value
            foreach (var draftPick in tradeOffer.DraftPicks)
            {
                switch (draftPick.Round)
                {
                    case 1:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_1);
                        break;
                    case 2:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_2);
                        break;
                    case 3:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_3);
                        break;
                    case 4:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_4);
                        break;
                    case 5:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_5);
                        break;
                    case 6:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_6);
                        break;
                    case 7:
                        tradeOfferLevels.Add(TradeOfferLevel.DRAFT_PICK_ROUND_7);
                        break;
                    default:
                        throw new Exception($"Unexpected round: {draftPick.Round}");
                }
            }

            return tradeOfferLevels;
        }

        private TradeResult CalculateTradeResult(IEnumerable<TradeOfferLevel> userOfferLevels, IEnumerable<TradeOfferLevel> computerOfferLevels)
        {
            // Calc if trade should be acceptd
            int playerTradeValue = 0;
            int computerTradeValue = 0;

            foreach (var playerLevel in userOfferLevels)
            {
                playerTradeValue += (int)playerLevel;
            }

            foreach (var computerLevel in computerOfferLevels)
            {
                computerTradeValue += (int)computerLevel;
            }

            // Player is offering more than the computer
            // TODO: Provide better reason
            if (playerTradeValue > computerTradeValue)
            {
                // Accept trade
                return new TradeResult(true, "Trade Accepted");
            }
            else
            {
                // Reject trade
                return new TradeResult(false, "Trade Rejected");
            }
        }
    }

    // Trade Values
    enum TradeOfferLevel
    {
        YOUNG_SUPER_STAR_PLAYER = 20,
        OLDER_SUPER_STAR_PLAYER = 13,
        STAR_PLAYER = 11,
        AVERAGE_PLAYER = 6,
        BAD_PLAYER = 2,

        DRAFT_PICK_ROUND_1 = 8,
        DRAFT_PICK_ROUND_2 = 7,
        DRAFT_PICK_ROUND_3 = 5,
        DRAFT_PICK_ROUND_4 = 4,
        DRAFT_PICK_ROUND_5 = 3,
        DRAFT_PICK_ROUND_6 = 2,
        DRAFT_PICK_ROUND_7 = 1,
    }
}
