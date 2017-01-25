using FluentAssertions;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Locators;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.BuildConfiguration
{
    [TestFixture]
    public class ProjectLocatorTests
    {
        [Test]
        public void By_Id()
        {
            // Arrange
            var locator = By.Project.Id("testId");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("project:(id:testId)");
        }

        [Test]
        public void By_Name()
        {
            // Arrange
            var locator = By.Project.Name("Some Name With Spaces & special characters");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("project:(name:Some+Name+With+Spaces+%26+special+characters)");
        }
    }
}
