using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.TestOccurrences
{
    [TestFixture]
    [Category("TestOccurrencesField")]
    public class TestOccurrencesFieldTests
    {
        [Test]
        public void Include_Count()
        {
            // Arrange
            var fields = Include.TestOccurrencesField.Count();
            // Act
            var query = fields.GetFieldsQueryString();
            // Assert
            query.Should().Be("count");
        }
        [Test]
        public void Include_Href()
        {
            var fields = Include.TestOccurrencesField.Href();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("href");
        }
        [Test]
        public void Include_Default()
        {
            var fields = Include.TestOccurrencesField;
            var query = fields.GetFieldsQueryString();

            query.Should().Be("count,href,testOccurrence(id,name,status,duration,href)");
        }

    }
}
