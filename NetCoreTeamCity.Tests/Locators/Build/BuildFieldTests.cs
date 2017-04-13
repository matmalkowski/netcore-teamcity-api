using FluentAssertions;
using NetCoreTeamCity.Api;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Locators.Build
{
    [TestFixture]
    public class BuildFieldTests
    {
        [Test]
        public void Include_Default()
        {
            // Arrange
            var fields = Include.Build.Default();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId,href,id,number,state,status,webUrl)");
        }

        [Test]
        public void Include_BuildTypeId()
        {
            // Arrange
            var fields = Include.Build.BuildTypeId();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildTypeId)");
        }

        [Test]
        public void Include_Href()
        {
            // Arrange
            var fields = Include.Build.Href();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(href)");
        }

        [Test]
        public void Include_Id()
        {
            // Arrange
            var fields = Include.Build.Id();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(id)");
        }

        [Test]
        public void Include_Number()
        {
            // Arrange
            var fields = Include.Build.Number();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(number)");
        }

        [Test]
        public void Include_State()
        {
            // Arrange
            var fields = Include.Build.State();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(state)");
        }

        [Test]
        public void Include_Status()
        {
            // Arrange
            var fields = Include.Build.Status();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(status)");
        }

        [Test]
        public void Include_WebUrl()
        {
            // Arrange
            var fields = Include.Build.WebUrl();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(webUrl)");
        }

        [Test]
        public void Include_BranchName()
        {
            // Arrange
            var fields = Include.Build.BranchName();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(branchName)");
        }


        [Test]
        public void Include_QueuedDate()
        {
            // Arrange
            var fields = Include.Build.QueuedDate();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(queuedDate)");
        }

        [Test]
        public void Include_StartDate()
        {
            // Arrange
            var fields = Include.Build.StartDate();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(startDate)");
        }

        [Test]
        public void Include_FinishDate()
        {
            // Arrange
            var fields = Include.Build.FinishDate();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(finishDate)");
        }

        [Test]
        public void Include_StatusText()
        {
            // Arrange
            var fields = Include.Build.StatusText();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(statusText)");
        }

        [Test]
        public void Include_Revisions()
        {
            // Arrange
            var fields = Include.Build.Revisions();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(revisions)");
        }

        [Test]
        public void Include_BuildType()
        {
            // Arrange
            var fields = Include.Build.BuildType();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(buildType)");
        }

        [Test]
        public void Include_Triggered()
        {
            // Arrange
            var fields = Include.Build.Triggered();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(triggered)");
        }

        [Test]
        public void Include_LastChanges()
        {
            // Arrange
            var fields = Include.Build.LastChanges();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(lastChanges(change))");
        }

        [Test]
        public void Include_Agent()
        {
            // Arrange
            var fields = Include.Build.Agent();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(agent)");
        }

        [Test]
        public void Include_Properties()
        {
            // Arrange
            var fields = Include.Build.Properties();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(properties(property))");
        }

        [Test]
        public void Include_TestOccurrences()
        {
            // Arrange
            var fields = Include.Build.TestOccurrences();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(testOccurrences)");
        }

        [Test]
        public void Include_PersonalFlag()
        {
            // Arrange
            var fields = Include.Build.PersonalFlag();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(personal)");
        }

        [Test]
        public void Include_Comment()
        {
            // Arrange
            var fields = Include.Build.Comment();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(comment)");
        }

        [Test]
        public void Include_RunningInfo()
        {
            // Arrange
            var fields = Include.Build.RunningInfo();

            // Act
            var query = fields.GetFieldsQueryString();

            // Assert
            query.Should().Be("build(running-info)");
        }
    }
}
