using System;
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
            var builds = queuedBuildService.Find(Include.Build.Default().StartDate());

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
            var builds = queuedBuildService.Find(By.BuildType.Id("123"), Include.Build.Default().QueuedDate());

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
            var builds = queuedBuildService.Find(By.Project.Id("123"), Include.Build.Default().QueuedDate());

            // Assert
            builds.Count.Should().Be(1);
            builds[0].Id.Should().Be(1);
        }

        [Test]
        public void GetCompatibleAgents_AgentsFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Agents>("buildQueue/123/compatibleAgents"))
                .Returns(new Agents() { Agent = new List<Agent> { new Agent() { Id = 1 } , new Agent() { Id = 2 } } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var agents = queuedBuildService.CompatibleAgents(123);

            // Assert
            agents.Count.Should().Be(2);
            agents[0].Id.Should().Be(1);
        }

        [Test]
        public void GetCompatibleAgents_AgentsNotFound()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Agents>("buildQueue/123/compatibleAgents"))
                .Returns(new Agents());

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var agents = queuedBuildService.CompatibleAgents(123);

            // Assert
            agents.Count.Should().Be(0);
        }

        [Test]
        public void EnqueueBuild_WithBuildType_QueuedBuildReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<BuildModel>("buildQueue", A<BuildModel>.That.Matches(b => b.BuildTypeId == "testBuildType")))
                .Returns(new BuildModel(){BuildTypeId = "testBuildType", Id = 123});

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var build = queuedBuildService.Run("testBuildType");

            // Assert
            build.Id.Should().Be(123);
            build.BuildTypeId.Should().Be("testBuildType");
            build.BranchName.Should().BeNullOrEmpty();
            build.Comment.Should().BeNull();
        }

        [Test]
        public void EnqueueBuild_WithBuildTypeAndBranch_QueuedBuildReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<BuildModel>("buildQueue", A<BuildModel>.That.Matches(b => (b.BuildTypeId == "testBuildType") && b.BranchName == "testBranchName")))
                .Returns(new BuildModel(){BuildTypeId = "testBuildType", Id = 123, BranchName = "testBranchName"});

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var build = queuedBuildService.Run("testBuildType", "testBranchName");

            // Assert
            build.Id.Should().Be(123);
            build.BuildTypeId.Should().Be("testBuildType");
            build.BranchName.Should().Be("testBranchName");
            build.Comment.Should().BeNull();
        }

        [Test]
        public void EnqueueBuild_WithBuildTypeAndBranchAndComment_QueuedBuildReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<BuildModel>("buildQueue", A<BuildModel>.That.Matches(
                b => b.BuildTypeId == "testBuildType" && b.BranchName == "testBranchName" && b.Comment.Text == "testComment")))
                .Returns(new BuildModel(){BuildTypeId = "testBuildType", Id = 123, BranchName = "testBranchName", Comment = new BuildComment(){Text = "testComment"}});

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var build = queuedBuildService.Run("testBuildType", "testBranchName", "testComment");

            // Assert
            build.Id.Should().Be(123);
            build.BuildTypeId.Should().Be("testBuildType");
            build.BranchName.Should().Be("testBranchName");
            build.Comment.Text.Should().Be("testComment");
        }

        [Test]
        public void EnqueueBuild_WithTriggerOptionsAndComment_QueuedBuildReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<BuildModel>("buildQueue", A<BuildModel>.That.Matches(
                b => b.BuildTypeId == "testBuildType" && b.BranchName == "testBranchName" && b.Comment.Text == "testComment" && b.TriggeringOptions.CleanSources == true)))
                .Returns(new BuildModel() { BuildTypeId = "testBuildType", Id = 123, BranchName = "testBranchName", Comment = new BuildComment() { Text = "testComment" } });

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            var build = queuedBuildService.Run(BuildRun.Options.BuildType("testBuildType").Branch("testBranchName").Comment("testComment").CleanSources());

            // Assert
            build.Id.Should().Be(123);
            build.BuildTypeId.Should().Be("testBuildType");
            build.BranchName.Should().Be("testBranchName");
            build.Comment.Text.Should().Be("testComment");
        }

        [Test]
        public void EnqueueBuild_NoOptionsPassed_ExceptionThrown()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            Action action = () => queuedBuildService.Run(null);

            action.Should().Throw<ArgumentNullException>();

        }

        [Test]
        public void CancelQueuedBuild()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            var buildService = new QueuedBuildService(teamCityApiClient);

            // Act
            buildService.Cancel(123, "Test");

            // Assert
            A.CallTo(() => teamCityApiClient.Post<BuildCancelRequest, BuildModel>("buildQueue/123", A<BuildCancelRequest>.Ignored))
                .MustHaveHappened();
        }

        [Test]
        public void GetQueuedBuild_BadRequest_ExceptionRethrown()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<BuildModel>("buildQueue/id:123")).Throws(new HttpException(HttpStatusCode.BadRequest));

            var queuedBuildService = new QueuedBuildService(teamCityApiClient);

            // Act
            Action action = () => queuedBuildService.Get(123);

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CancelRunningBuild_BadRequest_ExceptionRethrown()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<BuildCancelRequest, BuildModel>("buildQueue/123", A<BuildCancelRequest>.Ignored))
                .Throws(new HttpException(HttpStatusCode.BadRequest));

            var buildService = new QueuedBuildService(teamCityApiClient);

            // Act
            Action action = () => buildService.Cancel(123, "Test");

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
