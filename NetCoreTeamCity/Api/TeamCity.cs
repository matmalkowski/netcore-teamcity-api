using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Services;

namespace NetCoreTeamCity.Api
{
    public class TeamCity : ITeamCity
    {
        public TeamCity(string host, string userName, string password, bool usingSSL = true)
        {
            var connectionConfig = new TeamCityConnectionSettingsBuilder().ToHost(host).UsingSSL(usingSSL).AsUser(userName, password).Build();
            var bootstrapper = new BootStrapper(connectionConfig);

            Artefacts = bootstrapper.Get<IArtefactService>();
            Builds = bootstrapper.Get<IBuildService>();
            QueuedBuilds = bootstrapper.Get<IQueuedBuildService>();
            Changes = bootstrapper.Get<IChangeService>();
        }

        public IArtefactService Artefacts { get; }
        public IBuildService Builds { get; }
        public IQueuedBuildService QueuedBuilds { get; }
        public IChangeService Changes { get; }
    }
}
