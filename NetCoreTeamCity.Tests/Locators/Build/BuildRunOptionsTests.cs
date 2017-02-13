using System.Linq;
using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.Build
{
    [TestFixture]
    public class BuildRunOptionsTests
    {
        [Test]
        public void With_BuildType()
        {
            // Arrange
            var options = BuildRun.Options.BuildType("testBuildType");

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.BuildTypeId.Should().Be("testBuildType");
        }

        [Test]
        public void With_Branch()
        {
            // Arrange
            var options = BuildRun.Options.Branch("testBranchName");

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.BranchName.Should().Be("testBranchName");
        }

        [Test]
        public void With_Comment()
        {
            // Arrange
            var options = BuildRun.Options.Comment("testComment");

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.Comment.Text.Should().Be("testComment");
        }

        [Test]
        public void With_AsPersonal()
        {
            // Arrange
            var options = BuildRun.Options.AsPersonal();

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.Personal.Should().BeTrue();
        }

        [Test]
        public void With_OnAgent()
        {
            // Arrange
            var options = BuildRun.Options.OnAgent(123);

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.Agent.Id.Should().Be(123);
        }

        [Test]
        [Ignore("NotImplemented")]
        public void With_OnAgentPool()
        {
        }

        [Test]
        public void With_OnSpecificChange()
        {
            // Arrange
            var options = BuildRun.Options.OnSpecificChange(123);

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.LastChanges.Change.First().Id.Should().Be(123);
        }

        [Test]
        public void With_CleanSources()
        {
            // Arrange
            var options = BuildRun.Options.CleanSources();

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.TriggeringOptions.CleanSources.Should().BeTrue();
        }

        [Test]
        public void With_ForceRebuildAllDependencies()
        {
            // Arrange
            var options = BuildRun.Options.ForceRebuildAllDependencies();

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.TriggeringOptions.RebuildAllDependencies.Should().BeTrue();
        }

        [Test]
        public void With_MoveToTopOfQueue()
        {
            // Arrange
            var options = BuildRun.Options.MoveToTopOfQueue();

            // Act
            var build = options.GetBuildModel();

            // Assert
            build.TriggeringOptions.QueueAtTop.Should().BeTrue();
        }
    }
}
