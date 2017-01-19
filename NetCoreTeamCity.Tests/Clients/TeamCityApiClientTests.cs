using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using NetCoreTeamCity.Clients;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Models;
using Newtonsoft.Json;
using NetCoreTeamCity.Helpers;

namespace NetCoreTeamCity.Tests.Clients
{
    [TestFixture]
    public class TeamCityApiClientTests
    {
        private readonly DateTime DateUnderTest = new DateTime(2000, 12, 31, 1, 2, 3);
        private HttpResponseMessage JsonResponseWithOkStatus;
        private HttpResponseMessage JsonResponseWithErrorStatus;

        [OneTimeSetUp]
        public void SetUp()
        {
            var build = new BuildModel { BuildTypeId = "test", FinishDate = DateUnderTest };
            JsonResponseWithOkStatus = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(build, new TeamCityDateTimeConventer()))
            };
            JsonResponseWithErrorStatus = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(build, new TeamCityDateTimeConventer()))
            };
        }

        [Test]
        public void GetCall_AsGuest_ReturnsDeserializedJsonObject()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/guestAuth/app/rest/test", "application/json"))
                .Returns(JsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Get<BuildModel>("test");

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(DateUnderTest);
        }

        [Test]
        public void GetCall_AsAuthenticatedUser_ReturnsDeserializedJsonObject()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/httpAuth/app/rest/test", "application/json"))
                .Returns(JsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), false, "user", "password", true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Get<BuildModel>("test");

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(DateUnderTest);
        }

        [Test]
        public void GetCall_ResponseErrorThrowsException()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/guestAuth/app/rest/test", "application/json"))
                .Returns(JsonResponseWithErrorStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            Action action = () => tcApiClient.Get<BuildModel>("test");

            // Assert
            action.ShouldThrow<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void GetCall_NoAuthenticationProvidedForNonGuestCall_ExceptionThrown()
        {
            // Arrange
            var settings = new TeamCityConnectionSettings(new Uri("https://localhost"), false, string.Empty, string.Empty);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(A.Fake<IHttpClientWrapper>());

            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            Action action = () => tcApiClient.Get<BuildModel>("test");

            // Assert
            action.ShouldThrow<ArgumentException>().Which.Message.Should().Be("When connecting as guest you must specify username and password");
        }
    }
}
