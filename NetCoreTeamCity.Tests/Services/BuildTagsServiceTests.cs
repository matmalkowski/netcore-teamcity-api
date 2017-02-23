using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        [Test]
        public void Get_MultipleTagsReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Tags>("builds/123/tags/")).Returns(new Tags() { Tag = new List<Tag>() { new Tag() { Name = "tag1" }, new Tag() {Name = "tag2"} } });

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Get(123);

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(2);
            tags.Should().Contain("tag1");
            tags.Should().Contain("tag2");
        }

        [Test]
        public void Get_NoTags_EmptyListReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Tags>("builds/123/tags/")).Returns(new Tags());

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Get(123);

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Get_NullResponse_EmptyListReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Tags>("builds/123/tags/")).Returns(null);

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Get(123);

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Get_NotFound_EmptyListReturned()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Tags>("builds/123/tags/")).Throws(new HttpException(HttpStatusCode.NotFound));

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Get(123);

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Get_BadRequest_ExceptionRethrown()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Tags>("builds/123/tags/")).Throws(new HttpException(HttpStatusCode.BadRequest));

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            Action action = () => tagService.Get(123);

            // Assert
            action.ShouldThrow<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
