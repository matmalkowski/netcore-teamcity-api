using System;
using FluentAssertions;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Locators;
using NetCoreTeamCity.Models;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.Build
{
    [TestFixture]
    public class BuildLocatorTests
    {
        [Test]
        public void By_Id_Number_Tags_Status_QueuedAfter()
        {
            // Arrange
            var locator = By.Build
                .Id(123)
                .Number(456)
                .Tags("testTag1", "testTag2")
                .Status(BuildStatus.Failure)
                .QueuedDateAfter(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc))
                .QueuedDateAfter(By.Build.BuildType(By.BuildType.Id("123")));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("id:123,number:456,tag:testTag1,tag:testTag2,status:FAILURE,queuedDate:(date:20130305T170030%2B0000,condition:after),queuedDate:(buildType:(id:123),condition:after)");
        }

        [Test]
        public void By_Id()
        {
            // Arrange
            var locator = By.Build.Id(123);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("id:123");
        }

        [Test]
        public void By_Number()
        {
            // Arrange
            var locator = By.Build.Number(123);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("number:123");
        }

        [Test]
        public void By_Project()
        {
            // Arrange
            var locator = By.Build.Project("test_project_name");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("project:test_project_name");
        }

        [Test]
        public void By_AffectedProject()
        {
            // Arrange
            var locator = By.Build.AffectedProject("test_project_name");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("affectedProject:test_project_name");
        }

        [Test]
        public void By_BuildType_Id()
        {
            // Arrange
            var locator = By.Build.BuildType(By.BuildType.Id("123"));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("buildType:(id:123)");
        }

        [Test]
        public void By_BuildType_Name()
        {
            // Arrange
            var locator = By.Build.BuildType(By.BuildType.Name("123"));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("buildType:(name:123)");
        }

        [Test]
        public void By_Tag()
        {
            // Arrange
            var locator = By.Build.Tag("testTag");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("tag:testTag");
        }

        [Test]
        public void By_Tags()
        {
            // Arrange
            var locator = By.Build.Tags("testTag1", "testTag2");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("tag:testTag1,tag:testTag2");
        }

        [TestCase(BuildStatus.Success)]
        [TestCase(BuildStatus.Error)]
        [TestCase(BuildStatus.Failure)]
        public void By_Status(BuildStatus status)
        {
            // Arrange
            var locator = By.Build.Status(status);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"status:{status.ToString().ToUpper()}");
        }

        [Test]
        public void By_User()
        {
            // Arrange
            var locator = By.Build.User(By.User.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("user:(id:123)");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_PersonalFlag(Flag flag)
        {
            // Arrange
            var locator = By.Build.PersonalFlag(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"personal:{flag.ToString().ToLower()}");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_CanceledFlag(Flag flag)
        {
            // Arrange
            var locator = By.Build.CanceledFlag(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"canceled:{flag.ToString().ToLower()}");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_FailedToStartFlag(Flag flag)
        {
            // Arrange
            var locator = By.Build.FailedToStartFlag(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"failedToStart:{flag.ToString().ToLower()}");
        }

        [TestCase(BuildState.Finished)]
        [TestCase(BuildState.Queued)]
        [TestCase(BuildState.Running)]
        public void By_State(BuildState state)
        {
            // Arrange
            var locator = By.Build.State(state);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"state:{state.ToString().ToLower()}");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_RunningFlag(Flag flag)
        {
            // Arrange
            var locator = By.Build.RunningFlag(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"running:{flag.ToString().ToLower()}");
        }
        
        public void By_Hanging()
        {
            // Arrange
            var locator = By.Build.Hanging();

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"state:running,hanging:true");
        }

        public void By_Hanging_False()
        {
            // Arrange
            var locator = By.Build.Hanging(false);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().BeEmpty();
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_PinnedFlag(Flag flag)
        {
            // Arrange
            var locator = By.Build.PinnedFlag(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"pinned:{flag.ToString().ToLower()}");
        }

        [Test]
        public void By_Branch()
        {
            // Arrange
            var locator = By.Build.Branch("customName");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("branch:customName");
        }

        [Test]
        public void By_BranchWithSpecialChar()
        {
            // Arrange
            var locator = By.Build.Branch("customName/123");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("branch:customName%2F123");
        }

        [Test]
        public void By_BranchLocator()
        {
            // Arrange
            var locator = By.Build.Branch(By.Branch.Name("123"));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("branch:(name:123)");
        }

        [Test]
        public void By_Revision()
        {
            // Arrange
            var locator = By.Build.Revision("123");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("revision:123");
        }

        [Test]
        public void By_AgentName()
        {
            // Arrange
            var locator = By.Build.AgentName("123");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("agentName:123");
        }

        [Test]
        public void By_SinceBuild()
        {
            // Arrange
            var locator = By.Build.SinceBuild(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("sinceBuild:(id:123)");
        }

        [Test]
        public void By_Since()
        {
            // Arrange
            var locator = By.Build.Since(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("sinceDate:20130305T170030%2B0000");
        }

        [Test]
        public void By_QueuedDateBefore_By_BuildLocator()
        {
            // Arrange
            var locator = By.Build.QueuedDateBefore(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("queuedDate:(id:123,condition:before)");
        }

        [Test]
        public void By_QueuedDateAfter_By_BuildLocator()
        {
            // Arrange T+0400
            var locator = By.Build.QueuedDateAfter(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("queuedDate:(id:123,condition:after)");
        }

        [Test]
        public void By_QueuedDateBefore_By_Date()
        {
            // Arrange
            var locator = By.Build.QueuedDateBefore(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("queuedDate:(date:20130305T170030%2B0000,condition:before)");
        }

        [Test]
        public void By_QueuedDateAfter_By_Date()
        {
            // Arrange
            var locator = By.Build.QueuedDateAfter(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("queuedDate:(date:20130305T170030%2B0000,condition:after)");
        }

        [Test]
        public void By_StartDateBefore_By_BuildLocator()
        {
            // Arrange
            var locator = By.Build.StartDateBefore(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("startDate:(id:123,condition:before)");
        }

        [Test]
        public void By_StartDateAfter_By_BuildLocator()
        {
            // Arrange T+0400
            var locator = By.Build.StartDateAfter(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("startDate:(id:123,condition:after)");
        }

        [Test]
        public void By_StartDateBefore_By_Date()
        {
            // Arrange
            var locator = By.Build.StartDateBefore(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("startDate:(date:20130305T170030%2B0000,condition:before)");
        }

        [Test]
        public void By_StartDateAfter_By_Date()
        {
            // Arrange
            var locator = By.Build.StartDateAfter(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("startDate:(date:20130305T170030%2B0000,condition:after)");
        }

        [Test]
        public void By_FinishDateBefore_By_BuildLocator()
        {
            // Arrange
            var locator = By.Build.FinishDateBefore(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("finishDate:(id:123,condition:before)");
        }

        [Test]
        public void By_FinishDateAfter_By_BuildLocator()
        {
            // Arrange T+0400
            var locator = By.Build.FinishDateAfter(By.Build.Id(123));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("finishDate:(id:123,condition:after)");
        }

        [Test]
        public void By_FinishDateBefore_By_Date()
        {
            // Arrange
            var locator = By.Build.FinishDateBefore(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("finishDate:(date:20130305T170030%2B0000,condition:before)");
        }

        [Test]
        public void By_FinishDateAfter_By_Date()
        {
            // Arrange
            var locator = By.Build.FinishDateAfter(new DateTime(2013, 03, 05, 17, 00, 30, DateTimeKind.Utc));

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("finishDate:(date:20130305T170030%2B0000,condition:after)");
        }

        
    }
}
