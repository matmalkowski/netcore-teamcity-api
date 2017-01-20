﻿using System.Collections.Generic;
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

        [Test]
        public void GetBuilds_WithNoCriteria_WithNoIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("builds"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() {Id = 1}, new BuildModel() {Id = 2} } });

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var builds = buildService.Find();

            // Assert
            builds.Count.Should().Be(2);
            builds[1].Id.Should().Be(2);
        }

        [Test]
        public void GetBuilds_WithNoCriteria_NotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("builds"))
                .Returns(new Builds { Build = new List<BuildModel> () });

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var builds = buildService.Find();

            // Assert
            builds.Count.Should().Be(0);
        }

        [Test]
        public void GetBuilds_WithNoCriteria_WithIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("builds?fields=build(buildTypeId,href,id,number,state,status,webUrl,startDate)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 }, new BuildModel() { Id = 2 } } });

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var builds = buildService.Find(Include.Build.StartDate());

            // Assert
            builds.Count.Should().Be(2);
            builds[1].Id.Should().Be(2);
        }

        [Test]
        public void GetBuilds_WithCriteria_NotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("builds?locator=id:123"))
                .Returns(new Builds { Build = new List<BuildModel> () });

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var builds = buildService.Find(By.Build.Id(123));

            // Assert
            builds.Count.Should().Be(0);
        }

        [Test]
        public void GetBuilds_WithCriteria_WithNoIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("builds?locator=id:123"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 } } });

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var builds = buildService.Find(By.Build.Id(123));

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }

        [Test]
        public void GetBuilds_WithCriteria_WithIncludes()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Builds>("builds?locator=id:123&fields=build(buildTypeId,href,id,number,state,status,webUrl,startDate)"))
                .Returns(new Builds { Build = new List<BuildModel> { new BuildModel() { Id = 1 } } });

            var buildService = new BuildService(teamCityApiClient);

            // Act
            var builds = buildService.Find(By.Build.Id(123), Include.Build.StartDate());

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }
    }
}