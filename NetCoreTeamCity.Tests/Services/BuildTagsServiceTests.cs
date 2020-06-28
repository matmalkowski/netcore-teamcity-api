using System;
using System.Collections.Generic;
using System.Linq;
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
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Replace_SingleTag()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Put<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag[0].Name == "tag1")))
                .Returns(new Tags() {Tag = new List<Tag>() {new Tag() {Name = "tag1"} } });

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Replace(123, "tag1");

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(1);
            tags.FirstOrDefault().Should().Be("tag1");
        }

        [Test]
        public void Replace_MultipleTags()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Put<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag[0].Name == "tag1" && t.Tag[1].Name == "tag2")))
                .Returns(new Tags() { Tag = new List<Tag>() { new Tag() { Name = "tag1" }, new Tag() { Name = "tag2" } } });

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Replace(123, new List<string>() {"tag1", "tag2"});

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(2);
            tags[0].Should().Be("tag1");
            tags[1].Should().Be("tag2");
        }

        [Test]
        public void Replace_NoTags()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Put<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag.Count == 0)))
                .Returns(new Tags());

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Replace(123, new List<string>());

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Replace_NullPassedInsteadOfTagList()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            Action action = () => tagService.Replace(123, (string) null);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'tag')");
        }

        [Test]
        public void Replace_BuildNotFound_ExceptionRethrown()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Put<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag[0].Name == "tag1")))
                .Throws(new HttpException(HttpStatusCode.NotFound));

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            Action action = () => tagService.Replace(123, "tag1");

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void Add_SingleTag()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag[0].Name == "tag1")))
                .Returns(new Tags() { Tag = new List<Tag>() { new Tag() { Name = "tag1" } } });

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Add(123, "tag1");

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(1);
            tags.FirstOrDefault().Should().Be("tag1");
        }

        [Test]
        public void Add_MultipleTags()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag[0].Name == "tag1" && t.Tag[1].Name == "tag2")))
                .Returns(new Tags() { Tag = new List<Tag>() { new Tag() { Name = "tag1" }, new Tag() { Name = "tag2" } } });

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Add(123, new List<string>() { "tag1", "tag2" });

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(2);
            tags[0].Should().Be("tag1");
            tags[1].Should().Be("tag2");
        }

        [Test]
        public void Add_NoTags()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag.Count == 0)))
                .Returns(new Tags());

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            var tags = tagService.Add(123, new List<string>());

            // Assert
            tags.Should().NotBeNull();
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Add_NullPassedInsteadOfTagList()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            Action action = () => tagService.Add(123, (string)null);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'tag')");
        }

        [Test]
        public void Add_BuildNotFound_ExceptionRethrown()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Post<Tags>("builds/123/tags/", A<Tags>.That.Matches(t => t.Tag[0].Name == "tag1")))
                .Throws(new HttpException(HttpStatusCode.NotFound));

            var tagService = new BuildTagsService(teamCityApiClient);

            // Act
            Action action = () => tagService.Add(123, "tag1");

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
