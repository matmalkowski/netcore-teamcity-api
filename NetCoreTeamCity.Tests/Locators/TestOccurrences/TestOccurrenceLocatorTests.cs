using FluentAssertions;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Locators;
using NetCoreTeamCity.Locators.TestOccurrences;
using NetCoreTeamCity.Models;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.TestOccurrences
{
    [TestFixture]
    [Category("TestOccurrenceField")]
    public class TestOccurrenceLocatorTests
    {
        [Test]
        public void By_AffectedProject_Name()
        {
            // Arrange
            var locator = By.TestOccurences.AffectedProject("BuildTypeId");
            // Act
            var query = (locator as ILocator).GetLocatorQueryString();
            // Assert
            query.Should().Be("affectedProduct:BuildTypeId");
        }
        [Test]
        public void By_Branch()
        {
            var locator = By.TestOccurences.Branch("abc");
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("branch:abc");
        }
        [Test]
        public void By_Build()
        {
            var locator = By.TestOccurences.Build(By.Build.Id(1234).Branch("test"));
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("build:(id:1234,branch:test)");
        }
        [Test]
        public void By_CurrentlyFailing()
        {
            var locator = By.TestOccurences.CurrentlyFailing(true);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("currentlyFailing:True");
        }
        [Test]
        public void By_CurrentlyInvestigated()
        {
            var locator = By.TestOccurences.CurrentlyInvestigated(false);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("currentlyInvestigated:False");
        }
        [Test]
        public void By_CurrentlyMuted()
        {
            var locator = By.TestOccurences.CurrentlyMuted(true);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("currentlyMuted:True");
        }
        [Test]
        public void By_Id()
        {
            var locator = By.TestOccurences.Id(12345);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("id:12345");
        }
        [Test]
        public void By_Ignored()
        {
            var locator = By.TestOccurences.Ignored(true);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("ignored:True");
        }
        [Test]
        public void By_Muted()
        {
            var locator = By.TestOccurences.Muted(true);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("muted:True");
        }
        [Test]
        public void By_Status()
        {
            var locator = By.TestOccurences.Status(TestRunStatus.FAILURE);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("status:FAILURE");
        }
        [Test]
        public void By_Test()
        {
            var locator = By.TestOccurences.Test("test.name");
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("test:test.name");
        }
        [Test]
        public void By_Multiple()
        {
            var locator = By.TestOccurences.Build(By.Build.Id(123)).Muted(false).Status(TestRunStatus.FAILURE);
            var query = (locator as ILocator).GetLocatorQueryString();
            query.Should().Be("build:(id:123),muted:False,status:FAILURE");
        }
    }
}
