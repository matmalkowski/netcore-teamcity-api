using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators
{
    [TestFixture]
    public class BuildLocatorTests
    {
        [Test]
        public void ById()
        {
            // Arrange
            var locator = By.Build.Id(123);

            // Act
            var query = locator.GetLocatorQueryString();

            // Assert
            query.Should().Be("id:123");
        }
    }
}
