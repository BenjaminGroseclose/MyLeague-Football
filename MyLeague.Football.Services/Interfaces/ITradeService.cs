using MyLeague.Football.Services.Requests;
using MyLeague.Football.Services.Responses;

namespace MyLeague.Football.Services.Interfaces
{
    public interface ITradeService
    {
        /// <summary>
        /// Determines if a trade should or should be accepted by the computer
        /// </summary>
        /// <param name="playerOffer">The offer from the player's franchise <see cref="TradeOffer"/></param>
        /// <param name="computerOffer">The offer from the computer's franchise <see cref="TradeOffer"/></param>
        /// <returns>If the trade was accepted and the reason <see cref="TradeResult"/></returns>
        TradeResult ProposeTrade(TradeOffer playerOffer, TradeOffer computerOffer);
    }
}
