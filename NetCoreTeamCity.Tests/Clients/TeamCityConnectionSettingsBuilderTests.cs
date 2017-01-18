using System;
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
            settings.TeamCityHost.Should().Be(new Uri("http://localhost"));
            settings.Username.Should().Be("guest");
            settings.Password.Should().BeNullOrEmpty();
            settings.FavorJsonOverXml.Should().BeTrue();
        }

        [Test]
        public void ToHost_TeamCityHostSet()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.ToHost("testHost").Build();

            // Assert
            settings.TeamCityHost.Should().Be(new Uri("http://testHost"));
        }

        public void UsingSSL_TeamCityHostAddressUsingHttps()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.UsingSSL().Build();

            // Assert
            settings.TeamCityHost.Should().Be(new Uri("https://localhost"));
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
            settings.ConnectAsGuest.Should().BeFalse();
        }

        [Test]
        public void AsGuest_UserNameSetToGuest()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.AsGuest().Build();

            // Assert
            settings.Username.Should().Be("guest");
            settings.Password.Should().BeEmpty();
            settings.ConnectAsGuest.Should().BeTrue();
        }

        [Test]
        public void UseXML_FavorXmlFlagSet()
        {
            // Arrange
            var builder = new TeamCityConnectionSettingsBuilder();

            // Act
            var settings = builder.UsingXmlFormat().Build();

            // Assert
            settings.FavorJsonOverXml.Should().BeFalse();
        }
    }
}
