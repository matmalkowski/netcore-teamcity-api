using System;
using System.Net;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Services;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Services
{
    [TestFixture]
    public class BuildPinningServiceTests
    {
        [Test]
        public void PinBuildCalled_WithoutComment_BuildPinned()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            pinningService.Pin(123);

            // Assert
            A.CallTo(() => teamCityClientMock.Put<string>("builds/123/pin/", null)).MustHaveHappened();
        }

        [Test]
        public void PinBuildCalled_WithComment_BuildPinnedWithComment()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            pinningService.Pin(123, "some comment");

            // Assert
            A.CallTo(() => teamCityClientMock.Put<string>("builds/123/pin/", "some comment")).MustHaveHappened();
        }

        [Test]
        public void PinBuildCalled_InvalidBuildId_ExceptionThrown()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityClientMock.Put<string>("builds/123/pin/", null))
                .Throws(new HttpException(HttpStatusCode.NotFound));

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            Action action = () => pinningService.Pin(123);

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void UnpinBuildCalled_WithoutComment_BuildPinned()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            pinningService.UnPin(123);

            // Assert
            A.CallTo(() => teamCityClientMock.Delete<string>("builds/123/pin/", null)).MustHaveHappened();
        }

        [Test]
        public void UnpinBuildCalled_WithComment_BuildPinnedWithComment()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            pinningService.UnPin(123, "some comment");

            // Assert
            A.CallTo(() => teamCityClientMock.Delete("builds/123/pin/", "some comment")).MustHaveHappened();
        }

        [Test]
        public void UnpinBuildCalled_InvalidBuildId_ExceptionThrown()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityClientMock.Delete<string>("builds/123/pin/", null))
                .Throws(new HttpException(HttpStatusCode.NotFound));

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            Action action = () => pinningService.UnPin(123);

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void IsPinnedCalled_BuildPinned_TrueReturned()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityClientMock.Get<string>("builds/123/pin/"))
                .Returns("true");

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            var isPinned = pinningService.IsPinned(123);
            // Assert
            isPinned.Should().BeTrue();
        }

        [Test]
        public void IsPinnedCalled_BuildNotPinned_FalseReturned()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityClientMock.Get<string>("builds/123/pin/"))
                .Returns("false");

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            var isPinned = pinningService.IsPinned(123);
            // Assert
            isPinned.Should().BeFalse();
        }

        [Test]
        public void IsPinnedCalled_TeamCityReturnsUnexpectedResult_ExceptionThrown()
        {
            // Arrange
            var teamCityClientMock = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityClientMock.Get<string>("builds/123/pin/"))
                .Returns("someUnexptectedString");

            var pinningService = new BuildPinningService(teamCityClientMock);

            // Act
            Action action = () => pinningService.IsPinned(123);
            
            // Assert
            action.Should().Throw<UnexpectedApiResponseException>()
                .Which.Message.Should().Contain("TeamCity API response was \"someUnexptectedString\", exptected it to be \"true\"/\"false\"");
        }
    }
}
