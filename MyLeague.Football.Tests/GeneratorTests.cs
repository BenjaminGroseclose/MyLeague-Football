using MyLeague.Football.Data.Generators;
using NUnit.Framework;

namespace MyLeague.Football.Tests
{
    public class GeneratorTests
    {
        [Test]
        public void Test_GetPlayersName()
        {
            // Arrange
            string intialName = "DonaldAaron_10852";

            // Act
            var names = PlayerGenerator.GetPlayersName(intialName);

            // Assert
            Assert.AreEqual("Aaron", names.firstName);
            Assert.AreEqual("Donald", names.lastName);
        }

        [Test]
        public void Test_GetPlayersName_2()
        {
            // Arrange
            string intialName = "ThomasMichael_17552";

            // Act
            var names = PlayerGenerator.GetPlayersName(intialName);

            // Assert
            Assert.AreEqual("Michael", names.firstName);
            Assert.AreEqual("Thomas", names.lastName);
        }
    }
}
