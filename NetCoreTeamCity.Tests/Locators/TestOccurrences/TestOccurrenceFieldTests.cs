using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.TestOccurrences
{
    [TestFixture]
    public class TestOccurrenceFieldTests
    {
        [Test]
        public void Include_TestOccurrenceFieldDefault()
        {
            // Arrange
            var fields = Include.TestOccurrenceField;
            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("testOccurence(id,name,status,duratio,href)");
        }
        [Test]
        public void Test()
        {
            // Arrange
            var fields = Include.TestOccurrenceField.Test().Build();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("testOccurence(id,name,status,duratio,href,test)");
        }


    }
}
