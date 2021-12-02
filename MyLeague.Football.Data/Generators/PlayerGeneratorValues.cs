using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Generators
{
    public class PlayerGeneratorValues
    {
        public PlayerGeneratorValues(IEnumerable<Player> players, IEnumerable<PlayerAttributes> attributes)
        {
            this.Players = players;
            this.Attributes = attributes;
        }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<PlayerAttributes> Attributes { get; set; }
    }
}
