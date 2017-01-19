using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.Build
{
    [TestFixture]
    public class BuildLocatorTests
    {
        [Test]
        public void By_Id()
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
