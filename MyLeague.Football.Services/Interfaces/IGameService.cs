using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Services.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Advances the game by one week, simluating all games
        /// </summary>
        void AdvanceWeek();

        /// <summary>
        /// Advances the game by one season, simluating all games
        /// </summary>
        void AdvanceSeason();
    }
}
