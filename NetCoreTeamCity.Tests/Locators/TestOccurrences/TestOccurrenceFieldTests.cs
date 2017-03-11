using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.TestOccurrences
{
    [TestFixture]
    [Category("TestOccurrenceField")]
    public class TestOccurrenceFieldTests
    {
        [Test]
        public void Include_TestOccurrenceFieldDefault()
        {
            // Arrange
            var fields = Include.TestRun;
            // Act
            var query = fields.GetFieldsQueryString();
            // Assert
            query.Should().Be("testOccurrence(id,name,status,duration,href)");
        }
        [Test]
        public void Include_Test()
        {
            var fields = Include.TestRun.Test();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(test)");
        }
        [Test]
        public void Include_Build()
        {
            var fields = Include.TestRun.Build();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(build)");
        }
        [Test]
        public void Include_CurrentlyInvestigated()
        {
            var fields = Include.TestRun.CurrentlyInvestigated();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(currentlyInvestigated)");
        }
        [Test]
        public void Include_CurrentlyMuted()
        {
            var fields = Include.TestRun.CurrentlyMuted();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(currentlyMuted)");
        }
        [Test]
        public void Include_Details()
        {
            var fields = Include.TestRun.Details();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(details)");
        }
        [Test]
        public void Include_Duration()
        {
            var fields = Include.TestRun.Duration();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(duration)");
        }
        [Test]
        public void Include_Href()
        {
            var fields = Include.TestRun.Href();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(href)");
        }
        [Test]
        public void Include_Id()
        {
            var fields = Include.TestRun.Id();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(id)");
        }
        [Test]
        public void Include_Ignored()
        {
            var fields = Include.TestRun.Ignored();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(ignored)");
        }
        [Test]
        public void Include_Muted()
        {
            var fields = Include.TestRun.Muted();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(muted)");
        }
        [Test]
        public void Include_Name()
        {
            var fields = Include.TestRun.Name();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(name)");
        }
        [Test]
        public void Include_RunOrder()
        {
            var fields = Include.TestRun.RunOrder();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(runOrder)");
        }
        [Test]
        public void Include_Status()
        {
            var fields = Include.TestRun.Status();
            var query = fields.GetFieldsQueryString();

            query.Should().Be("testOccurrence(status)");
        }
        
    }
}
