using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
