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
        private readonly List<TestOccurrence> defaultReturn = new List<TestOccurrence>
        {
            new TestOccurrence {Id="id:123,build:(id:123456)", Name="Test1", Status=TestRunStatus.SUCCESS, Duration=100 , Href="Test1Herf", Muted=false},
            new TestOccurrence {Id="id:124,build:(id:123456)", Name="Test2", Status=TestRunStatus.FAILURE, Duration=12 , Href="Test2Herf", Muted=false }
        };
        [Test]
        public void Get_SingleTestOccurrence()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestOccurrence>($"{endpoint}/id:123,build:(id:123456)")).Returns(new TestOccurrence
            {
                Id = "id:123,build:(id:123456)",
                Build = new Build() { Id = 123456 },
                Test = new Test() { Id = 123, Name = "UnitTest" }

            });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccObj = testOccurenceService.Get("id:123,build:(id:123456)");

            // Assert
            testOccObj.Should().NotBeNull();
            testOccObj.Id.Should().Be("id:123,build:(id:123456)");
            testOccObj.Build.Id.Should().Be(123456);
            testOccObj.Test.Id.Should().Be(123);
            testOccObj.Test.Name.Should().Be("UnitTest");
        }

        [Test]
        public void Find_ByBuildId_NoField()
        {
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestRunsModel>($"{endpoint}?locator=build:(id:123456)"))
                .Returns(new TestRunsModel
                {
                    TestOccurrences = defaultReturn
                });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccList = testOccurenceService.Find(By.Build.Id(123456), count: 0);

            // Assert
            testOccList.Should().NotBeNull();
            testOccList.Count.Should().Be(2);
            testOccList[0].Id.Should().Be("id:123,build:(id:123456)");
            testOccList[1].Id.Should().Be("id:124,build:(id:123456)");
        }
        [Test]
        public void Find_ByBuildId_NotFound()
        {
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestRunsModel>($"{endpoint}?locator=build:(id:123456)"))
                .Returns(new TestRunsModel
                {
                    TestOccurrences = defaultReturn
                });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccList = testOccurenceService.Find(By.Build.Id(123457), count: 0);

            // Assert
            testOccList.Should().NotBeNull();
            testOccList.Count.Should().Be(0);
        }
        [Test]
        public void Find_ByBuildId_FieldSpecific()
        {
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestRunsModel>($"{endpoint}?locator=count:100,build:(id:123456)&fields=testOccurrence(id,name,status,duration,href)"))
                .Returns(new TestRunsModel
                {
                    TestOccurrences = defaultReturn
                });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccList = testOccurenceService.Find(By.Build.Id(123456), Include.TestRun.Id().Name().Status().Duration().Href());

            // Assert
            testOccList.Should().NotBeNull();
            testOccList.Count.Should().Be(2);
            testOccList[0].ShouldBeEquivalentTo(defaultReturn[0]);
            testOccList[1].ShouldBeEquivalentTo(defaultReturn[1]);
        }
        [Test]
        public void Find_ByBuildId_StatusFilter_FieldSpecific()
        {
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestRunsModel>($"{endpoint}?locator=count:100,build:(id:123456),status:FAILURE&fields=testOccurrence(id,name,status,duration,href)"))
                .Returns(new TestRunsModel
                {
                    TestOccurrences = defaultReturn.Where(to => to.Status.Equals(TestRunStatus.FAILURE)).ToList()
                });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccList = testOccurenceService.Find(By.TestOccurences.Build(
                By.Build.Id(123456)).Status(TestRunStatus.FAILURE),
                Include.TestRun.Id().Name().Status().Duration().Href()
            );

            // Assert
            testOccList.Should().NotBeNull();
            testOccList.Count.Should().Be(1);
            testOccList[0].ShouldBeEquivalentTo(defaultReturn[1]);
        }
        [Test]
        public void Find_ByBuildId_MultipleFilter_FieldSpecific()
        {
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<TestRunsModel>($"{endpoint}?locator=count:100,build:(id:123456),status:FAILURE,mute:ture&fields=testOccurrence(id,name,status,duration,href)"))
                .Returns(new TestRunsModel
                {
                    TestOccurrences = defaultReturn.Where(to => to.Status.Equals(TestRunStatus.FAILURE) && to.Muted == true).ToList()
                });

            var testOccurenceService = new TestOccurrencesService(teamCityApiClient);

            // Act
            var testOccList = testOccurenceService.Find(By.TestOccurences.Build(
                By.Build.Id(123456)).Status(TestRunStatus.FAILURE),
                Include.TestRun.Id().Name().Status().Duration().Href()
            );

            // Assert
            testOccList.Should().NotBeNull();
            testOccList.Count.Should().Be(0);
        }
    }
}
