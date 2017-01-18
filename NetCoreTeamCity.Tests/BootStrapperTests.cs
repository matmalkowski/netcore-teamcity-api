using FakeItEasy;
using NUnit.Framework;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Services;
using FluentAssertions;

namespace NetCoreTeamCity.Tests
{
    [TestFixture]
    public class BootStrapperTests
    {
        [Test]
        public void GetTeamCityService_FakeConnectionSettings_NotNull()
        {
            // Arrange
            var bootstrapper = new BootStrapper(A.Fake<ITeamCityConnectionSettings>());

            // Act
            var teamCityService = bootstrapper.Get<ITeamCityService>();

            // Assert
            teamCityService.Should().NotBeNull();
        }
    }
}
