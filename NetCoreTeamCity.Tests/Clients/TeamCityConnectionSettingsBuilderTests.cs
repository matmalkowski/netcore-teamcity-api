using FluentAssertions;
using NUnit.Framework;
using NetCoreTeamCity.Clients;

namespace NetCoreTeamCity.Tests.Clients
{
    [TestFixture]
    public class TeamCityConnectionSettingsBuilderTests
    {
        [Test]
        public void DefaultValuesCorrect()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.Build();

            // Assert
            settings.TeamCityHost.Should().Be("localhost");
            settings.Username.Should().Be("guest");
            settings.Password.Should().BeNullOrEmpty();
        }

        [Test]
        public void ToHost_TeamCityHostSet()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.ToHost("testHost").Build();

            // Assert
            settings.TeamCityHost.Should().Be("testHost");
        }

        [Test]
        public void AsUser_UserNameAndPasswordSet()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.AsUser("user", "secret").Build();

            // Assert
            settings.Username.Should().Be("user");
            settings.Password.Should().Be("secret");
        }
    }
}
