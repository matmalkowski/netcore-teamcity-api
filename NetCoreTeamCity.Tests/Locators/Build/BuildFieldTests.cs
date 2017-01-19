using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.Build
{
    [TestFixture]
    public class BuildFieldTests
    {
        [Test]
        public void Include_StartDate()
        {
            // Arrange
            var fields = Include.Build.StartDate();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,startDate)");
        }

        [Test]
        public void Include_FinishDate()
        {
            // Arrange
            var fields = Include.Build.FinishDate();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,finishDate)");
        }

        [Test]
        public void Include_StatusText()
        {
            // Arrange
            var fields = Include.Build.StatusText();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,statusText)");
        }

        [Test]
        public void Include_Revisions()
        {
            // Arrange
            var fields = Include.Build.Revisions();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,revisions)");
        }

        [Test]
        public void Include_BuildType()
        {
            // Arrange
            var fields = Include.Build.BuildType();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,buildType)");
        }

        [Test]
        public void Include_Triggered()
        {
            // Arrange
            var fields = Include.Build.Triggered();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,triggered)");
        }

        [Test]
        public void Include_LastChanges()
        {
            // Arrange
            var fields = Include.Build.LastChanges();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,lastChanges(change))");
        }

        [Test]
        public void Include_Agent()
        {
            // Arrange
            var fields = Include.Build.Agent();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,agent)");
        }

        [Test]
        public void Include_Properties()
        {
            // Arrange
            var fields = Include.Build.Properties();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl,properties(property))");
        }


    }
}
