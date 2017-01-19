using System.Net;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Services
{
    [TestFixture]
    public class BuildServiceTests
    {
        [Test]
        public void GetBuild_Id_BuildRetrived()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<BuildModel>("builds/id:123")).Returns(new BuildModel() {Id = 123});

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var build = buildService.Get(123);

            // Assert
            build.Should().NotBeNull();
            build.Id.Should().Be(123);
        }

        [Test]
        public void GetBuild_Id_BuildNotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<BuildModel>("builds/id:123")).Throws(new HttpException(HttpStatusCode.NotFound));

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var build = buildService.Get(123);

            // Assert
            build.Should().BeNull();
        }
    }
}
