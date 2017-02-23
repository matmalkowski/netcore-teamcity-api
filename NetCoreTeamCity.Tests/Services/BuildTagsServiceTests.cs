using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Services
{
    [TestFixture]
    public class BuildTagsServiceTests
    {
        [Test]
        public void Get_SingleTagReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Tags>("builds/123/tags/")).Returns(new Tags() { Tag = new List<Tag>() {new Tag() {Name = "tag1"} } });

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Get(123);

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(1);
            tags.Should().Contain("tag1");
        }
    }
}
