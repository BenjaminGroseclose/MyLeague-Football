using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Services.Requests
{
    public class TradeOffer
    {
        public TradeOffer(Franchise franchise, List<Player> players, List<DraftPick> draftPicks)
        {
            this.Franchise = franchise;
            this.Players = players;
            this.DraftPicks = draftPicks;
        }

        public Franchise Franchise { get; set; }
        public List<Player> Players { get; set; }
        public List<DraftPick> DraftPicks { get; set; }
    }
}
