using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;
using NUnit.Framework;
using NetCoreTeamCity.Api;

namespace NetCoreTeamCity.Tests.Services
{
    [TestFixture]
    public class TestOccurencesServiceTests
    {
        private readonly string endpoint = "testOccurrences";
        [Test]
        public void Get_SingleTestOccurrence()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestOccurrence>($"{endpoint}/id:123,build:(id:123456)")).Returns(new TestOccurrence
            {
                Id = "id:123,build:(id:123456)",
                Build = new Build() { Id = 123456 },
                Test = new Test() { Id = 4015036523514658854, Name = "UnitTest" }

            });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccObj = testOccurenceService.Get("id:123,build:(id:123456)");

            // Assert
            testOccObj.Should().NotBeNull();
            testOccObj.Id.Should().Be("id:123,build:(id:123456)");
            testOccObj.Build.Id.Should().Be(123456);
            testOccObj.Test.Id.Should().Be(4015036523514658854);
            testOccObj.Test.Name.Should().Be("UnitTest");
        }

        [Test]
        public void Find_ByBuildId_NoField()
        {
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestOccurrences>($"{endpoint}?locator=build:(id:123456)"))
                .Returns(new TestOccurrences
                {
                    TestOccurrenceItems = new List<TestOccurrence>
                    {
                        new TestOccurrence {Id="id:123,build:(id:123456)" },
                        new TestOccurrence {Id="id:124,build:(id:123456)" }
                    }
                });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccList = testOccurenceService.Find(By.Build.Id(123456));

            // Assert
            testOccList.Should().NotBeNull();
            testOccList.Count.Should().Be(2);
            testOccList[0].Id.Should().Be("id:123,build:(id:123456)");
            testOccList[1].Id.Should().Be("id:124,build:(id:123456)");
        }
       
    }
}
