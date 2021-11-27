using MyLeague.Football.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MyLeague.Football.Data.Generators
{
    public static class FranchiseGenerator
    {
        public static IEnumerable<Franchise> CreateDefaultFranchises()
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            string basePath = Path.GetDirectoryName(assembly);

            List<Franchise> franchises = new List<Franchise>()
            {
                new Franchise(1, "Chicago", "Bears", "CHI", $"{basePath}/Generators/TeamLogos/chicago-bears.png", "0B162A", "C83803", Conference.NFC, Division.NORTH),
                new Franchise(2, "Cincinnati", "Bengals", "CIN", $"{basePath}/Generators/TeamLogos/cincinnati-bengals.png", "FB4F14", "000000", Conference.AFC, Division.NORTH),
                new Franchise(3, "Buffalo", "Bills", "BUF", $"{basePath}/Generators/TeamLogos/buffalo-bills.png", "00338D", "C60C30", Conference.AFC, Division.EAST),
                new Franchise(4, "Denver", "Broncos", "DEN", $"{basePath}/Generators/TeamLogos/denver-broncos.png", "002244", "FB4F14", Conference.AFC, Division.WEST),
                new Franchise(5, "Cleveland", "Browns", "CLE", $"{basePath}/Generators/TeamLogos/cleveland-browns.png", "FB4F14", "22150C", Conference.AFC, Division.NORTH),
                new Franchise(6, "Tampa Bay", "Buccaneers", "TB", $"{basePath}/Generators/TeamLogos/tampa-bay-buccaneers.png", "D50A0A", "34302B", Conference.NFC, Division.SOUTH),
                new Franchise(7, "Arizona", "Cardinals", "ARI", $"{basePath}/Generators/TeamLogos/arizona-cardinals.png", "97233F", "000000", Conference.NFC, Division.WEST),
                new Franchise(8, "Los Angeles", "Chargers", "LAC", $"{basePath}/Generators/TeamLogos/los-angeles-chargers.png", "0073CF", "FFB612", Conference.AFC, Division.WEST),
                new Franchise(9, "Kansas City", "Chiefs", "KC", $"{basePath}/Generators/TeamLogos/kansas-city-chiefs.png", "E31837", "FFB612", Conference.AFC, Division.WEST),
                new Franchise(10, "Indianapolis", "Colts", "IND", $"{basePath}/Generators/TeamLogos/indianapolis-colts.png", "002C5F", "A5ACAF", Conference.AFC, Division.SOUTH),
                new Franchise(11, "Dallas", "Cowboys", "DAL", $"{basePath}/Generators/TeamLogos/dallas-cowboys.png", "002244", "B0B7BC", Conference.NFC, Division.EAST),
                new Franchise(12, "Miami", "Dolphins", "MIA", $"{basePath}/Generators/TeamLogos/miami-dolphins.png", "008E97", "F58220", Conference.AFC, Division.EAST),
                new Franchise(13, "Philadelphia", "Eagles", "PHI", $"{basePath}/Generators/TeamLogos/philadelphia-eagles.png", "004953", "A5ACAF", Conference.NFC, Division.EAST),
                new Franchise(14, "Atlanta", "Falcons", "ATL", $"{basePath}/Generators/TeamLogos/atlanta-falcons.png", "A71930", "000000", Conference.NFC, Division.SOUTH),
                new Franchise(15, "San Francisco", "49ers", "SF", $"{basePath}/Generators/TeamLogos/san-francisco-49ers.png", "AA0000", "B3995D", Conference.NFC, Division.WEST),
                new Franchise(16, "New York", "Giants", "NYG", $"{basePath}/Generators/TeamLogos/new-york-giants.png", "0B2265", "A71930", Conference.NFC, Division.EAST),
                new Franchise(17, "Jacksonville", "Jaguars", "JAX", $"{basePath}/Generators/TeamLogos/jacksonville-jaguars.png", "006778", "000000", Conference.AFC, Division.SOUTH),
                new Franchise(18, "New York", "Jets", "NYJ", $"{basePath}/Generators/TeamLogos/new-york-jets.png", "203731", "FFFFFF", Conference.AFC, Division.EAST),
                new Franchise(19, "Detroit", "Lions", "DET", $"{basePath}/Generators/TeamLogos/detroit-lions.png", "005A8B", "B0B7BC", Conference.NFC, Division.NORTH),
                new Franchise(20, "Green Bay", "Packers", "GB", $"{basePath}/Generators/TeamLogos/green-bay-packers.png", "203731", "FFB612", Conference.NFC, Division.NORTH),
                new Franchise(21, "Carolina", "Panthers", "CAR", $"{basePath}/Generators/TeamLogos/carolina-panthers.png", "0085CA", "000000", Conference.NFC, Division.SOUTH),
                new Franchise(22, "New England", "Patriots", "NE", $"{basePath}/Generators/TeamLogos/new-england-patriots.png", "002244", "C60C30", Conference.AFC, Division.EAST),
                new Franchise(23, "Las Vegas", "Raiders", "OAK", $"{basePath}/Generators/TeamLogos/oakland-raiders.png", "A5ACAF", "000000", Conference.AFC, Division.WEST),
                new Franchise(24, "Los Angeles", "Rams", "LAR", $"{basePath}/Generators/TeamLogos/los-angeles-rams.png", "002244", "B3995D", Conference.NFC, Division.WEST),
                new Franchise(25, "Baltimore", "Ravens", "BAL", $"{basePath}/Generators/TeamLogos/baltimore-ravens.png", "241773", "9E7C0C", Conference.AFC, Division.NORTH),
                new Franchise(26, "Washington", "Football Team", "WAS", $"{basePath}/Generators/TeamLogos/washington-football-team.png", "773141", "FFB612", Conference.NFC, Division.EAST),
                new Franchise(27, "New Orleans", "Saints", "NO", $"{basePath}/Generators/TeamLogos/new-orleans-saints.png", "9F8958", "000000", Conference.NFC, Division.SOUTH),
                new Franchise(28, "Seattle", "Seahawks", "SEA", $"{basePath}/Generators/TeamLogos/seattle-seahawks.png", "002244", "69BE28", Conference.NFC, Division.WEST),
                new Franchise(29, "Pittsburgh", "Steelers", "PIT", $"{basePath}/Generators/TeamLogos/pittsburgh-steelers.png", "000000", "FFB612", Conference.AFC, Division.NORTH),
                new Franchise(30, "Tennessee", "Titans", "TEN", $"{basePath}/Generators/TeamLogos/tennessee-titans.png", "002244", "4B92DB", Conference.AFC, Division.SOUTH),
                new Franchise(31, "Minnesota", "Vikings", "MIN", $"{basePath}/Generators/TeamLogos/minnesota-vikings.png", "4F2683", "FFC62F", Conference.NFC, Division.NORTH),
                new Franchise(32, "Houston", "Texans", "HOU", $"{basePath}/Generators/TeamLogos/houston-texans.png", "03202F", "A71930", Conference.AFC, Division.SOUTH)
            };

            return franchises;
        }
    }
}
