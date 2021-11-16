using MyLeague.Football.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.Generators
{
    public static class FranchiseGenerator
    {
        public static async Task CreateDefaultFranchises(MyLeagueFootballContext context)
        {
            List<Franchise> franchises = new List<Franchise>()
            {
                new Franchise(1, "Chicago", "Bears", "CHI", "chicago-bears.svg", "0B162A", "C83803"),
                new Franchise(2, "Cincinnati", "Bengals", "CIN", "cincinnati-bengals.svg", "FB4F14", "000000"),
                new Franchise(3, "Buffalo", "Bills", "BUF", "buffalo-bills.svg", "00338D", "C60C30"),
                new Franchise(4, "Denver", "Broncos", "DEN", "denver-broncos.svg", "002244", "FB4F14"),
                new Franchise(5, "Cleveland", "Browns", "CLE", "cleveland-browns.svg", "FB4F14", "22150C"),
                new Franchise(6, "Tampa Bay", "Buccaneers", "TB", "tampa-bay-buccaneers.svg", "D50A0A", "34302B"),
                new Franchise(7, "Arizona", "Cardinals", "ARI", "arizona-cardinals.svg", "97233F", "000000"),
                new Franchise(8, "Los Angeles", "Chargers", "LAC", "los-angeles-chargers.svg", "0073CF", "FFB612"),
                new Franchise(9, "Kansas City", "Chiefs", "KC", "kansas-city-chiefs.svg", "E31837", "FFB612"),
                new Franchise(10, "Indianapolis", "Colts", "IND", "indianapolis-colts.svg", "002C5F", "A5ACAF"),
                new Franchise(11, "Dallas", "Cowboys", "DAL", "dallas-cowboys.svg", "002244", "B0B7BC"),
                new Franchise(12, "Miami", "Dolphins", "MIA", "miami-dolphins.svg", "008E97", "F58220"),
                new Franchise(13, "Philadelphia", "Eagles", "PHI", "philadelphia-eagles.svg", "004953", "A5ACAF"),
                new Franchise(14, "Atlanta", "Falcons", "ATL", "atlanta-falcons.svg", "A71930", "000000"),
                new Franchise(15, "San Francisco", "49ers", "SF", "san-francisco-49ers.svg", "AA0000", "B3995D"),
                new Franchise(16, "New York", "Giants", "NYG", "new-york-giants.svg", "0B2265", "A71930"),
                new Franchise(17, "Jacksonville", "Jaguars", "JAX", "jacksonville-jaguars.svg", "006778", "000000"),
                new Franchise(18, "New York", "Jets", "NYJ", "new-york-jets.svg", "203731", "FFFFFF"),
                new Franchise(19, "Detroit", "Lions", "DET", "detroit-lions.svg", "005A8B", "B0B7BC"),
                new Franchise(20, "Green Bay", "Packers", "GB", "green-bay-packers.svg", "203731", "FFB612"),
                new Franchise(21, "Carolina", "Panthers", "CAR", "carolina-panthers.svg", "0085CA", "000000"),
                new Franchise(22, "New England", "Patriots", "NE", "new-england-patriots.svg", "002244", "C60C30"),
                new Franchise(23, "Las Vegas", "Raiders", "OAK", "oakland-raiders.svg", "A5ACAF", "000000"),
                new Franchise(24, "Los Angeles", "Rams", "LAR", "los-angeles-rams.svg", "002244", "B3995D"),
                new Franchise(25, "Baltimore", "Ravens", "BAL", "baltimore-ravens.svg", "241773", "9E7C0C"),
                new Franchise(26, "Washington", "Football Team", "WAS", "washington-football-team.png", "773141", "FFB612"),
                new Franchise(27, "New Orleans", "Saints", "NO", "new-orleans-saints.svg", "9F8958", "000000"),
                new Franchise(28, "Seattle", "Seahawks", "SEA", "seattle-seahawks.svg", "002244", "69BE28"),
                new Franchise(29, "Pittsburgh", "Steelers", "PIT", "pittsburgh-steelers.svg", "000000", "FFB612"),
                new Franchise(30, "Tennessee", "Titans", "TEN", "tennessee-titans.svg", "002244", "4B92DB"),
                new Franchise(31, "Minnesota", "Vikings", "MIN", "minnesota-vikings.svg", "4F2683", "FFC62F"),
                new Franchise(32, "Houston", "Texans", "HOU", "houston-texans.svg", "03202F", "A71930")
            };

            context.Franchises.AddRange(franchises);
            await context.SaveChangesAsync();
        }
    }
}
