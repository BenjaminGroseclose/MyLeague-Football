using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.API.Models
{
    public class SportsDataSchedule
    {
        public string GameKey { get; set; }
        public int SeasonType { get; set; }
        public int Season { get; set; }
        public int Week { get; set; }
        public string Date { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeam { get; set; }
        public string Channel { get; set; }
        public decimal? PointSpread { get; set; }
        public decimal? OverUnder { get; set; }
        public int? StadiumID { get; set; }
        public bool? Canceled { get; set; }
        public decimal? GeoLat { get; set; }
        public int? ForecastTempLow  { get; set; }
        public int? ForecastTempHigh { get; set; }
    }
}
