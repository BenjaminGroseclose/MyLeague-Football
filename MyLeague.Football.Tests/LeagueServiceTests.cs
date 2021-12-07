using Moq;
using MyLeague.Football.Data.API;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Implementations;
using NUnit.Framework;

namespace MyLeague.Football.Tests
{
    internal class LeagueServiceTests
    {
        LeagueService sut;

        Mock<IFranchiseRepository> franchiseRepository;
        Mock<ILeagueRepository> leagueRepository;
        Mock<ISportsDataAPI> sportsDateAPI;
        Mock<IScheduleRepository> scheduleRepository;
        Mock<IRecordsRepository> recordsRepository;

        [SetUp]
        public void Setup()
        {
            this.franchiseRepository = new Mock<IFranchiseRepository>();
            this.leagueRepository = new Mock<ILeagueRepository>();
            this.sportsDateAPI = new Mock<ISportsDataAPI>();
            this.scheduleRepository = new Mock<IScheduleRepository>();
            this.recordsRepository = new Mock<IRecordsRepository>();

            this.sut = new LeagueService(this.franchiseRepository.Object, this.leagueRepository.Object, this.sportsDateAPI.Object, this.scheduleRepository.Object, this.recordsRepository.Object);
        }

        /*
         * Each team plays twice against each of the other three teams in its division: once at home, and once on the road (six games).
         * Each team plays once against each of the four teams from a predetermined division (based on a three-year rotation) within its own conference: two at home, and two on the road (four games).
         * Each team plays once against each team from the remaining two divisions within its conference that finished in a similar placement in the final divisional standings in the prior season[a]: one at home, one on the road (two games).
         * Each team plays once against each of the four teams from a predetermined division (based on a four-year rotation) in the other conference: two at home, and two on the road (four games).
         * Each team also plays one game against the team from a predetermined division (based on a four-year rotation) in the other conference that finished in a similar placement in the final divisional standings in the prior season[a] (one game).
         
        [Test]
        public void Schedule_HappyPath()
        {
            // Arrange
            var franchises = FranchiseGenerator.CreateDefaultFranchises();

            // Act
            var result = this.sut.GenerateSchedule(franchises, 2021);

            foreach (var franchise in franchises)
            {
                var franchiseResult = result.Where(x => x.HomeTeamId == franchise.Id || x.AwayTeamId == franchise.Id).ToList();

                Assert.AreEqual(franchiseResult.Count, 18);
            }

            var filteredResult = result.Where(x => x.HomeTeamId == 29 || x.AwayTeamId == 29).ToList();

            // Assert
            Assert.AreEqual(result.Count(), 18);
            Assert.AreEqual(filteredResult.Where(x => x.HomeTeamId == 29 && x.AwayTeam?.Conference == Conference.AFC && x.AwayTeam.Division == Division.NORTH).Count(), 3);
            Assert.AreEqual(filteredResult.Where(x => x.AwayTeamId == 29 && x.HomeTeam.Conference == Conference.AFC && x.HomeTeam.Division == Division.NORTH).Count(), 3);
            Assert.IsTrue(filteredResult.Where(x => x.HomeTeamId == 29).Count() >= 8);
            Assert.IsTrue(filteredResult.Where(x => x.AwayTeamId == 29).Count() >= 8);
            Assert.AreEqual(filteredResult.Where(x => x.HomeTeamId == 29 || x.AwayTeamId == 29).Count(), 17);
        }
        */
    }
}
