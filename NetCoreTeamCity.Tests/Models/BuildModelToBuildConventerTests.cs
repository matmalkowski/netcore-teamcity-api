using System;
using System.Collections.Generic;
using FluentAssertions;
using NetCoreTeamCity.Models;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Models
{
    [TestFixture]
    public class BuildModelToBuildConventerTests
    {
        [Test]
        public void BuildModel_ConvertTo_Build()
        {
            // Arrange
            var buildModel = new BuildModel()
            {
                Id = 123,
                Number = "1.2.3",
                Status = BuildStatus.Success,
                State = BuildState.Finished,
                BuildTypeId = "testType",
                Href = "href",
                WebUrl = "url",
                StatusText = "status",
                StartDate = new DateTime(2000, 1, 1),
                FinishDate = new DateTime(2000, 1, 2),
                QueuedDate = new DateTime(2000, 1, 3),
                BuildType = new BuildConfiguration {Description = "buildTypeDescription"},
                Triggered = new Triggered {Date = new DateTime(2000, 1, 1)},
                Agent = new Agent {Id = 123}
            };

            // Act
            var build = buildModel.Convert();

            // Assert
            build.Id.Should().Be(123);
            build.Number.Should().Be("1.2.3");
            build.Status.Should().Be(BuildStatus.Success);
            build.State.Should().Be(BuildState.Finished);
            build.BuildTypeId.Should().Be("testType");
            build.Href.Should().Be("href");
            build.StatusText.Should().Be("status");
            build.StartDate.Should().Be(new DateTime(2000, 1, 1));
            build.FinishDate.Should().Be(new DateTime(2000, 1, 2));
            build.QueuedDate.Should().Be(new DateTime(2000, 1, 3));
            build.BuildType.Description.Should().Be("buildTypeDescription");
            build.Triggered.Date.Should().Be(new DateTime(2000, 1, 1));
            build.Agent.Id.Should().Be(123);
        }

        [Test]
        public void BuildModel_LastChanges_ConvertTo_Build()
        {
            // Arrange
            var buildModel = new BuildModel
            {
                LastChanges =
                    new LastChanges {Change = new List<Change>() {new Change() {Id = 1}, new Change() {Id = 2}}}
            };

            // Act
            var build = buildModel.Convert();

            // Assert
            build.LastChanges.Should().NotBeNull();
            build.LastChanges.Count.Should().Be(2);
            build.LastChanges[1].Id.Should().Be(2);
        }

        [Test]
        public void BuildModel_Revisions_ConvertTo_Build()
        {
            // Arrange
            var buildModel = new BuildModel
            {
                Revisions =
                    new Revisions { Revision = new List<Revision>() { new Revision() { Version = "1" }, new Revision() { Version = "2" } } }
            };

            // Act
            var build = buildModel.Convert();

            // Assert
            build.Revisions.Should().NotBeNull();
            build.Revisions.Count.Should().Be(2);
            build.Revisions[1].Version.Should().Be("2");
        }

        [Test]
        public void BuildModel_Properties_ConvertTo_Build()
        {
            // Arrange
            var buildModel = new BuildModel
            {
                Properties = 
                    new Properties { Property = new List<Property>() { new Property() { Name = "1" }, new Property() { Name = "2" } } }
            };

            // Act
            var build = buildModel.Convert();

            // Assert
            build.Properties.Should().NotBeNull();
            build.Properties.Count.Should().Be(2);
            build.Properties[1].Name.Should().Be("2");
        }
    }
}
