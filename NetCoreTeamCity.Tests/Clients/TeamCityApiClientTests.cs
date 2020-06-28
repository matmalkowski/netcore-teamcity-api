﻿using System;
using System.Net;
using System.Net.Http;
using Castle.Core.Internal;
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
        private readonly DateTime _dateUnderTest = new DateTime(2000, 12, 31, 1, 2, 3);
        private HttpResponseMessage _jsonResponseWithOkStatus;
        private HttpResponseMessage _jsonResponseWithErrorStatus;
        private StringContent _buildAsSerializedStringContent;

        [OneTimeSetUp]
        public void SetUp()
        {
            var build = new BuildModel { BuildTypeId = "test", FinishDate = _dateUnderTest };
            _buildAsSerializedStringContent = new StringContent(JsonConvert.SerializeObject(build, new TeamCityDateTimeConventer()));
            _jsonResponseWithOkStatus = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = _buildAsSerializedStringContent
            };
            _jsonResponseWithErrorStatus = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = _buildAsSerializedStringContent
            };
        }

        [Test]
        public void GetCall_AsGuest_ReturnsPlainTextResponse()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/guestAuth/app/rest/test", "text/plain"))
                .Returns(new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("testPlainTextResponse")});
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Get<string>("test");

            // Assert
            result.Should().NotBeNull()
                .And.Be("testPlainTextResponse");
        }

        [Test]
        public void GetCall_AsGuest_ReturnsDeserializedJsonObject()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/guestAuth/app/rest/test", "application/json"))
                .Returns(_jsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Get<BuildModel>("test");

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(_dateUnderTest);
        }

        [Test]
        public void GetCall_AsAuthenticatedUser_ReturnsDeserializedJsonObject()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/httpAuth/app/rest/test", "application/json"))
                .Returns(_jsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), false, "user", "password", true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Get<BuildModel>("test");

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(_dateUnderTest);
        }

        [Test]
        public void GetCall_ResponseErrorThrowsException()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Get("https://fake/guestAuth/app/rest/test", "application/json"))
                .Returns(_jsonResponseWithErrorStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            Action action = () => tcApiClient.Get<BuildModel>("test");

            // Assert
            action.Should().Throw<HttpException>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
            action.Should().Throw<ArgumentException>().Which.Message.Should().Be("When connecting as guest you must specify username and password");
        }

        [Test]
        public void PostCall_SerializedObjectPassedInCall_DeserializedObjectReturned()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Post("https://fake/guestAuth/app/rest/test", A<StringContent>.That.Matches(c => c.ReadAsStringAsync().Result.Contains("testObjectSerialization")), "application/json"))
                .Returns(_jsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Post("test", new BuildModel { BuildTypeId = "testObjectSerialization", FinishDate = _dateUnderTest });

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(_dateUnderTest);
        }

        [Test]
        public void PostCall_StringPassedInCall_DeserializedObjectReturned()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Post("https://fake/guestAuth/app/rest/test", A<StringContent>.That.Matches(c => c.ReadAsStringAsync().Result.Contains("somePlainText")), "application/json"))
                .Returns(_jsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Post<string, BuildModel>("test", "somePlainText");

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(_dateUnderTest);
        }

        [Test]
        public void PostCall_SerializedObjectPassedInCall_StringReturned()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Post("https://fake/guestAuth/app/rest/test", A<StringContent>.That.Matches(c => c.ReadAsStringAsync().Result.Contains("testObjectSerialization")), "text/plain"))
                .Returns(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("testPlainTextResponse") });
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Post<BuildModel, string>("test", new BuildModel { BuildTypeId = "testObjectSerialization", FinishDate = _dateUnderTest });

            // Assert
            result.Should().NotBeNull()
                .And.Be("testPlainTextResponse");
        }

        [Test]
        public void PutCall_SerializedObjectPassedInCall_DeserializedObjectReturned()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClient.Put("https://fake/guestAuth/app/rest/test", A<StringContent>.That.Matches(c => c.ReadAsStringAsync().Result.Contains("testObjectSerialization")), "application/json"))
                .Returns(_jsonResponseWithOkStatus);
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            var result = tcApiClient.Put("test", new BuildModel { BuildTypeId = "testObjectSerialization", FinishDate = _dateUnderTest });

            // Assert
            result.Should().NotBeNull();
            result.BuildTypeId.Should().Be("test");
            result.FinishDate.Should().Be(_dateUnderTest);
        }

        [Test]
        public void DeleteCall_NothingPassed_CallMade()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
               
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            tcApiClient.Delete<string>("test", null);

            // Assert
            A.CallTo(() => httpClient.Delete("https://fake/guestAuth/app/rest/test", A<StringContent>.That.Matches(c => c.ReadAsStringAsync().Result.IsNullOrEmpty()), "text/plain")).MustHaveHappened();
        }

        [Test]
        public void DeleteCall_SerializedObjectPassedInCall_DeserializedObjectReturned()
        {
            // Arrange
            var httpClient = A.Fake<IHttpClientWrapper>();
            var httpClientFactory = A.Fake<IHttpClientWrapperFactory>();
            A.CallTo(() => httpClientFactory.Create()).Returns(httpClient);

            var settings = new TeamCityConnectionSettings(new Uri("https://fake"), true, "guest", string.Empty, true);
            var tcApiClient = new TeamCityApiClient(settings, httpClientFactory);

            // Act
            tcApiClient.Delete("test", new BuildModel { BuildTypeId = "testObjectSerialization", FinishDate = _dateUnderTest });

            // Assert
            A.CallTo(() => httpClient.Delete("https://fake/guestAuth/app/rest/test",
                    A<StringContent>.That.Matches(c => c.ReadAsStringAsync().Result.Contains("testObjectSerialization")),
                    "application/json"))
                .MustHaveHappened();

        }
    }
}
