using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.User
{
    [TestFixture]
    public class UserLocatorTests
    {
        [Test]
        public void By_Id()
        {
            // Arrange
            var locator = By.User.Id(123);

            // Act
            var query = locator.GetLocatorQueryString();

            // Assert
            query.Should().Be("id:123");
        }

        [Test]
        public void By_Name()
        {
            // Arrange
            var locator = By.User.Name("123");

            // Act
            var query = locator.GetLocatorQueryString();

            // Assert
            query.Should().Be("name:123");
        }
    }
}
