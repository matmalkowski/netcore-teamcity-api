using System.Collections.Generic;
using System.Net;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Services
{
    [TestFixture]
    public class QueuedBuildServiceTests
    {
        [Test]
        public void GetQueuedBuild_Id_BuildRetrived()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<BuildModel>("buildQueue/id:123")).Returns(new BuildModel() {Id = 123});

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var build = queuedBuildService.Get(123);

            // Assert
            build.Should().NotBeNull();
            build.Id.Should().Be(123);
        }

        [Test]
        public void GetQueuedBuild_Id_BuildNotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<BuildModel>("buildQueue/id:123")).Throws(new HttpException(HttpStatusCode.NotFound));

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var build = queuedBuildService.Get(123);

            // Assert
            build.Should().BeNull();
        }

        [Test]
        public void GetQueuedBuilds_WithNoCriteria_WithNoIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() {Id = 1}, new BuildModel() {Id = 2} } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find();

            // Assert
            builds.Count.Should().Be(2);
            builds[1].Id.Should().Be(2);
        }

        [Test]
        public void GetQueuedBuilds_WithNoCriteria_NotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100"))
                .Returns(new Builds { Build = new List<BuildModel> () });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find();

            // Assert
            builds.Count.Should().Be(0);
        }

        [Test]
        public void GetQueuedBuilds_WithNoCriteria_WithIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100&fields=build(buildTypeId,href,id,number,state,status,webUrl,startDate)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 }, new BuildModel() { Id = 2 } } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(Include.Build.StartDate());

            // Assert
            builds.Count.Should().Be(2);
            builds[1].Id.Should().Be(2);
        }

        [Test]
        public void GetQueuedBuilds_WithBuildTypeCriteria_NotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100,buildType:(id:123)"))
                .Returns(new Builds { Build = new List<BuildModel> () });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(By.BuildType.Id("123"));

            // Assert
            builds.Count.Should().Be(0);
        }

        [Test]
        public void GetQueuedBuilds_WithBuildTypeCriteria_WithNoIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100,buildType:(name:123+456)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 } } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(By.BuildType.Name("123 456"));

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }

        [Test]
        public void GetQueuedBuilds_WithBuildTypeCriteria_WithIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100,buildType:(id:123)&fields=build(buildTypeId,href,id,number,state,status,webUrl,queuedDate)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 } } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(By.BuildType.Id("123"), Include.Build.QueuedDate());

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }

        [Test]
        public void GetQueuedBuilds_WithProjectCriteria_NotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100,project:(id:123)"))
                .Returns(new Builds { Build = new List<BuildModel>() });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(By.Project.Id("123"));

            // Assert
            builds.Count.Should().Be(0);
        }

        [Test]
        public void GetQueuedBuilds_WithProjectCriteria_WithNoIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100,project:(name:123+456)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 } } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(By.Project.Name("123 456"));

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }

        [Test]
        public void GetQueuedBuilds_WithProjectCriteria_WithIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("buildQueue?locator=count:100,project:(id:123)&fields=build(buildTypeId,href,id,number,state,status,webUrl,queuedDate)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 } } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var builds = queuedBuildService.Find(By.Project.Id("123"), Include.Build.QueuedDate());

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }
    }
}
