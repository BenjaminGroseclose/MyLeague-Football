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
                new Franchise(1, "Chicago", "Bears", "CHI", "/Assets/TeamLogos/chicago-bears.svg", "0B162A", "C83803", Conference.NFC, Division.NORTH),
                new Franchise(2, "Cincinnati", "Bengals", "CIN", "/Assets/TeamLogos/cincinnati-bengals.svg", "FB4F14", "000000", Conference.AFC, Division.NORTH),
                new Franchise(3, "Buffalo", "Bills", "BUF", "/Assets/TeamLogos/buffalo-bills.svg", "00338D", "C60C30", Conference.AFC, Division.EAST),
                new Franchise(4, "Denver", "Broncos", "DEN", "/Assets/TeamLogos/denver-broncos.svg", "002244", "FB4F14", Conference.AFC, Division.WEST),
                new Franchise(5, "Cleveland", "Browns", "CLE", "/Assets/TeamLogos/cleveland-browns.svg", "FB4F14", "22150C", Conference.AFC, Division.NORTH),
                new Franchise(6, "Tampa Bay", "Buccaneers", "TB", "/Assets/TeamLogos/tampa-bay-buccaneers.svg", "D50A0A", "34302B", Conference.NFC, Division.SOUTH),
                new Franchise(7, "Arizona", "Cardinals", "ARI", "/Assets/TeamLogos/arizona-cardinals.svg", "97233F", "000000", Conference.NFC, Division.WEST),
                new Franchise(8, "Los Angeles", "Chargers", "LAC", "/Assets/TeamLogos/los-angeles-chargers.svg", "0073CF", "FFB612", Conference.AFC, Division.WEST),
                new Franchise(9, "Kansas City", "Chiefs", "KC", "/Assets/TeamLogos/kansas-city-chiefs.svg", "E31837", "FFB612", Conference.AFC, Division.WEST),
                new Franchise(10, "Indianapolis", "Colts", "IND", "/Assets/TeamLogos/indianapolis-colts.svg", "002C5F", "A5ACAF", Conference.AFC, Division.SOUTH),
                new Franchise(11, "Dallas", "Cowboys", "DAL", "/Assets/TeamLogos/dallas-cowboys.svg", "002244", "B0B7BC", Conference.NFC, Division.EAST),
                new Franchise(12, "Miami", "Dolphins", "MIA", "/Assets/TeamLogos/miami-dolphins.svg", "008E97", "F58220", Conference.AFC, Division.EAST),
                new Franchise(13, "Philadelphia", "Eagles", "PHI", "/Assets/TeamLogos/philadelphia-eagles.svg", "004953", "A5ACAF", Conference.NFC, Division.EAST),
                new Franchise(14, "Atlanta", "Falcons", "ATL", "/Assets/TeamLogos/atlanta-falcons.svg", "A71930", "000000", Conference.NFC, Division.SOUTH),
                new Franchise(15, "San Francisco", "49ers", "SF", "/Assets/TeamLogos/san-francisco-49ers.svg", "AA0000", "B3995D", Conference.NFC, Division.WEST),
                new Franchise(16, "New York", "Giants", "NYG", "/Assets/TeamLogos/new-york-giants.svg", "0B2265", "A71930", Conference.NFC, Division.EAST),
                new Franchise(17, "Jacksonville", "Jaguars", "JAX", "/Assets/TeamLogos/jacksonville-jaguars.svg", "006778", "000000", Conference.AFC, Division.SOUTH),
                new Franchise(18, "New York", "Jets", "NYJ", "/Assets/TeamLogos/new-york-jets.svg", "203731", "FFFFFF", Conference.AFC, Division.EAST),
                new Franchise(19, "Detroit", "Lions", "DET", "/Assets/TeamLogos/detroit-lions.svg", "005A8B", "B0B7BC", Conference.NFC, Division.NORTH),
                new Franchise(20, "Green Bay", "Packers", "GB", "/Assets/TeamLogos/green-bay-packers.svg", "203731", "FFB612", Conference.NFC, Division.NORTH),
                new Franchise(21, "Carolina", "Panthers", "CAR", "/Assets/TeamLogos/carolina-panthers.svg", "0085CA", "000000", Conference.NFC, Division.SOUTH),
                new Franchise(22, "New England", "Patriots", "NE", "/Assets/TeamLogos/new-england-patriots.svg", "002244", "C60C30", Conference.AFC, Division.EAST),
                new Franchise(23, "Las Vegas", "Raiders", "OAK", "/Assets/TeamLogos/oakland-raiders.svg", "A5ACAF", "000000", Conference.AFC, Division.WEST),
                new Franchise(24, "Los Angeles", "Rams", "LAR", "/Assets/TeamLogos/los-angeles-rams.svg", "002244", "B3995D", Conference.NFC, Division.WEST),
                new Franchise(25, "Baltimore", "Ravens", "BAL", "/Assets/TeamLogos/baltimore-ravens.svg", "241773", "9E7C0C", Conference.AFC, Division.NORTH),
                new Franchise(26, "Washington", "Football Team", "WAS", "/Assets/TeamLogos/washington-football-team.png", "773141", "FFB612", Conference.NFC, Division.EAST),
                new Franchise(27, "New Orleans", "Saints", "NO", "/Assets/TeamLogos/new-orleans-saints.svg", "9F8958", "000000", Conference.NFC, Division.SOUTH),
                new Franchise(28, "Seattle", "Seahawks", "SEA", "/Assets/TeamLogos/seattle-seahawks.svg", "002244", "69BE28", Conference.NFC, Division.WEST),
                new Franchise(29, "Pittsburgh", "Steelers", "PIT", "/Assets/TeamLogos/pittsburgh-steelers.svg", "000000", "FFB612", Conference.AFC, Division.NORTH),
                new Franchise(30, "Tennessee", "Titans", "TEN", "/Assets/TeamLogos/tennessee-titans.svg", "002244", "4B92DB", Conference.AFC, Division.SOUTH),
                new Franchise(31, "Minnesota", "Vikings", "MIN", "/Assets/TeamLogos/minnesota-vikings.svg", "4F2683", "FFC62F", Conference.NFC, Division.NORTH),
                new Franchise(32, "Houston", "Texans", "HOU", "/Assets/TeamLogos/houston-texans.svg", "03202F", "A71930", Conference.AFC, Division.SOUTH)
            };

            context.Franchises.AddRange(franchises);
            await context.SaveChangesAsync();
        }
    }
}
