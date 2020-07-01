using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Services
{
    public class ArtefactServiceTests
    {
        [Test]
        public void FindBuild_Id_ArtifactRetrieved()
        {
            // Arrange
            var teamCityApiClient = A.Fake<ITeamCityApiClient>();
            A.CallTo(() => teamCityApiClient.Get<Artifacts>("builds/count:10,buildType:(id:buildtypeid)/artifacts"))
                .Returns(new Artifacts() {File = new List<ArtifactModel>() {new ArtifactModel() {Name = "expectedName"}}});

            var service = new ArtefactService(teamCityApiClient);

            // Act
            var artifacts = service.Find(By.Build.BuildType(By.BuildType.Id("buildtypeid")));

            // Assert
            artifacts.FirstOrDefault().Should().NotBeNull();
            artifacts.FirstOrDefault()?.Name.Should().Be("expectedName");
        }
    }
}