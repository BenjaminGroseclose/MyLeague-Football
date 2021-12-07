using Moq;
using MyLeague.Football.Data.Generators;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Implementations;
using MyLeague.Football.Services.Requests;
using MyLeague.Football.Services.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.Tests
{
    public class TradeServiceTests
    {
        private TradeService sut;
        private Mock<ILeagueRepository> leagueRepository;
        private Mock<IFranchiseRepository> franchiseRepository;
        private Mock<IPlayerRepository> playerRepository;
        private IEnumerable<Player> playerList;
        private IEnumerable<Franchise> franchiseList;
        private DateTime leagueDate = new DateTime(2021, 8, 16);

        [SetUp]
        public void SetUp()
        {
            // Load mock data
            this.playerList = this.GetPlayerList();
            this.franchiseList = this.GetFranchiseList(this.playerList);

            this.leagueRepository = new Mock<ILeagueRepository>();
            this.franchiseRepository = new Mock<IFranchiseRepository>();
            this.playerRepository = new Mock<IPlayerRepository>();

            this.leagueRepository.Setup(x => x.GetLeague(1)).Returns(new League(1, new Franchise(), leagueDate, "Ben", "Tester", 2021, 1));
            this.playerRepository.Setup(x => x.UpdatePlayer(It.IsAny<int>(), It.IsAny<Player>())).Returns(new Player());
            this.franchiseRepository.Setup(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()));

            this.sut = new TradeService(this.leagueRepository.Object, this.franchiseRepository.Object, this.playerRepository.Object);
        }

        // TDD

        // ------ ACCEPT TRADE TESTS -----
        // ------ YOUNG SUPER STAR LEVEL ------

        [Test]
        public void ShouldAcceptTrade_YoungerSuper_TwoFirstRounders()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id),
                new DraftPick(1, 2022, 1, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_YoungerSuperStar_OlderSuperStar()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_YoungerSuperStar_Star()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        //// ----- OLD SUPER STAR LEVEL -----

        [Test]
        public void ShouldAcceptTrade_OlderSuperStar_OneFirstRounders()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_OlderSuperStar_2ndAnd3rdRound()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 2, franchise.Id),
                new DraftPick(1, 2021, 3, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_OlderSuperStar_Star()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        //// ----- STAR LEVEL -----

        [Test]
        public void ShouldAcceptTrade_Star_3rdAnd4thRound()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 3, franchise.Id),
                new DraftPick(1, 2021, 4, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_Star_Two4thRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 3, franchise.Id),
                new DraftPick(1, 2022, 4, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_Star_1stAnd6thRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id),
                new DraftPick(1, 2021, 6, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_StarLevelAverage()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.AveragePlayer() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        //// ----- AVERAGE LEVEL ------

        [Test]
        public void ShouldAcceptTrade_Average_3rdRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.AveragePlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 3, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_Average_4thAnd7thRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.AveragePlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 4, franchise.Id),
                new DraftPick(0, 2021, 7, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldAcceptTrade_AverageLevelBad()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.AveragePlayer() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.BadPlayer() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        //// ----- BAD LEVEL -----

        [Test]
        public void ShouldAcceptTrade_BadLevel()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.BadPlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 7, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        //// ----- REJECT TRADE TESTS -----
        //// ----- YOUNG SUPER START LEVEL -----

        [Test]
        public void ShouldRejectTrade_Two1stRounders_YoungSuperStar()
        { 
            // Arrange
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var userOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id),
                new DraftPick(1, 2022, 1, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        [Test]
        public void ShouldRejectTrade_YoungSuperStar_Three1stRounders()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id),
                new DraftPick(1, 2022, 1, franchise.Id),
                new DraftPick(2, 2023, 1, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        //// ----- OLDER SUPER STAR -----

        [Test]
        public void ShouldRejectTrade_OlderSuperStar_Two1stRounders()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id),
                new DraftPick(1, 2022, 1, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }


        [Test]
        public void ShouldRejectTrade_OlderSuperStar_YoungSuperStar()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        //// ----- STAR LEVEL -----

        [Test]
        public void ShouldRejectTrade_Star_2ndAnd3rdRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 2, franchise.Id),
                new DraftPick(1, 2021, 3, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        [Test]
        public void ShouldRejectTrade_Star_3rd4thAnd5thRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 3, franchise.Id),
                new DraftPick(1, 2021, 4, franchise.Id),
                new DraftPick(1, 2021, 5, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        [Test]
        public void ShouldRejectTrade_Star_SuperStar()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        //// ----- AVERAGE LEVEL -----

        [Test]
        public void ShouldRejectTrade_Average_2ndRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.AveragePlayer() }, new List<DraftPick>());

            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 2, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        [Test]
        public void ShouldRejectTrade_AverageLevelStar()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.AveragePlayer() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.StarPlayer() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        //// ----- MISC -----

        [Test]
        public void ShouldRejectTrade_VeryClearly_1stRounder()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.BadPlayer() }, new List<DraftPick>());
            Franchise franchise = this.RandomFranchise();

            var computerOffer = new TradeOffer(franchise, new List<Player>(), new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise.Id)
            });

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        [Test]
        public void ShouldRejectTrade_VeryClearly_YoungStar()
        {
            // Arrange
            TradeOffer userOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.BadPlayer() }, new List<DraftPick>());
            TradeOffer computerOffer = new TradeOffer(this.RandomFranchise(), new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>());

            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
            this.franchiseRepository.Verify(x => x.UpdateDraftPicks(It.IsAny<int>(), It.IsAny<DraftPick>()), Times.Never);
        }

        //// COMPLICATED TRADES
        
        [Test]
        public void ShouldAccept_ComplicatedTrade()
        {
            Franchise franchise1 = this.RandomFranchise();
            TradeOffer userOffer = new TradeOffer(franchise1, new List<Player>() { this.StarPlayer() }, new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise1.Id),
                new DraftPick(0, 2021, 3, franchise1.Id)
            });

            Franchise franchise2 = this.RandomFranchise();
            TradeOffer computerOffer = new TradeOffer(franchise2, new List<Player>() { this.YoungSuperStar() }, new List<DraftPick>()
            {
                new DraftPick(0, 2021, 5, franchise2.Id)
            });
            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsTrue(result.IsAccepted);
            Assert.AreEqual("Trade Accepted", result.Reason);
        }

        [Test]
        public void ShouldReject_ComplicatedTrade()
        {
            Franchise franchise1 = this.RandomFranchise();
            TradeOffer userOffer = new TradeOffer(franchise1, new List<Player>() { this.StarPlayer() }, new List<DraftPick>()
            {
                new DraftPick(0, 2021, 2, franchise1.Id)
            });

            Franchise franchise2 = this.RandomFranchise();
            TradeOffer computerOffer = new TradeOffer(franchise2, new List<Player>() { this.OlderSuperStar() }, new List<DraftPick>()
            {
                new DraftPick(0, 2021, 1, franchise1.Id),
                new DraftPick(0, 2021, 5, franchise2.Id)
            });
            // Act
            var result = this.sut.ProposeTrade(userOffer, computerOffer);

            // Assert
            Assert.IsFalse(result.IsAccepted);
            Assert.AreEqual("Trade Rejected", result.Reason);
        }

        //// NULL Check Tests

        [Test]
        public void Trade_PlayerNull_Throws()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => this.sut.ProposeTrade(null, new TradeOffer(null, null, null)));
        }

        [Test]
        public void Trade_ComputerNull_Throws()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => this.sut.ProposeTrade(new TradeOffer(null, null, null), null));
        }


        private Player YoungSuperStar()
        {

            return this.playerList.FirstOrDefault(x => x.Age(this.leagueDate) <= 28 && x.PlayerAttributes.Overall >= 95);
        }

        private Player OlderSuperStar()
        {
            return this.playerList.FirstOrDefault(x => x.Age(this.leagueDate) > 28 && x.PlayerAttributes.Overall >= 95);
        }

        private Player StarPlayer()
        {
            return this.playerList.FirstOrDefault(x => x.PlayerAttributes.Overall >= 90 && x.PlayerAttributes.Overall < 95);
        }

        private Player AveragePlayer()
        {
            return this.playerList.FirstOrDefault(x => x.PlayerAttributes.Overall >= 80 && x.PlayerAttributes.Overall < 90);
        }

        private Player BadPlayer()
        {
            return this.playerList.FirstOrDefault(x => x.PlayerAttributes.Overall < 80);
        }

        private IEnumerable<Player> GetPlayerList()
        {
            var generatedPlayers = PlayerGenerator.CreateDefaultPlayer();

            foreach(var player in generatedPlayers.Players)
            {
                player.PlayerAttributes = generatedPlayers.Attributes.First(x => x.Id == player.PlayerAttributeId);
            }

            return generatedPlayers.Players;
        }

        private IEnumerable<Franchise> GetFranchiseList(IEnumerable<Player> players)
        {
            var franchises = FranchiseGenerator.CreateDefaultFranchises();

            foreach (var franchise in franchises)
            {
                franchise.Players = players.Where(x => x.FranchiseId == franchise.Id);
            }

            return franchises;
        }

        private Franchise RandomFranchise()
        {
            Random rand = new Random();
            var randomId = rand.Next(1, 32);
            return this.franchiseList.First(x => x.Id == randomId);
        }
    }
}
