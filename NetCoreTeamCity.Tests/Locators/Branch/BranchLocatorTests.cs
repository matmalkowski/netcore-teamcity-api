using FluentAssertions;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Locators;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.Branch
{
    [TestFixture]
    public class BranchLocatorTests
    {
        [Test]
        public void By_Name()
        {
            // Arrange
            var locator = By.Branch.Name("123");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("name:123");
        }

        [Test]
        public void By_NameWithSpecialCharacter()
        {
            // Arrange
            var locator = By.Branch.Name("pull/123");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("name:pull%2F123");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_Default(Flag flag)
        {
            // Arrange
            var locator = By.Branch.Default(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"default:{flag.ToString().ToLower()}");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_Branched(Flag flag)
        {
            // Arrange
            var locator = By.Branch.Branched(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"branched:{flag.ToString().ToLower()}");
        }

        [TestCase(Flag.Any)]
        [TestCase(Flag.True)]
        [TestCase(Flag.False)]
        public void By_Unspecified(Flag flag)
        {
            // Arrange
            var locator = By.Branch.Unspecified(flag);

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be($"unspecified:{flag.ToString().ToLower()}");
        }

        [Test]
        public void By_Combined()
        {
            // Arrange
            var locator = By.Branch.Unspecified(Flag.Any).Name("123");

            // Act
            var query = (locator as ILocator).GetLocatorQueryString();

            // Assert
            query.Should().Be("unspecified:any,name:123");
        }
    }
}
