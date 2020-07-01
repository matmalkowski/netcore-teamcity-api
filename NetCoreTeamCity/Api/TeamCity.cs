using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Services;

namespace NetCoreTeamCity.Api
{
    public class TeamCity : ITeamCity
    {
        public TeamCity(string host, string userName, string password, bool usingSSL = true)
        {
            var connectionConfig = new TeamCityConnectionSettingsBuilder().ToHost(host).UsingSSL(usingSSL).AsUser(userName, password).Build();
            Init(connectionConfig);
        }

        public TeamCity(string host, string token, bool usingSSL = true)
        {
            var connectionConfig = new TeamCityConnectionSettingsBuilder().ToHost(host).UsingSSL(usingSSL).WithToken(token).Build();
            Init(connectionConfig);
        }

        public IArtefactService Artefacts { get; private set;}
        public IBuildService Builds { get; private set;}
        public IQueuedBuildService QueuedBuilds { get; private set;}
        public IChangeService Changes { get; private set;}
        
        private void Init(ITeamCityConnectionSettings connectionConfig)
        {
            var bootstrapper = new BootStrapper(connectionConfig);
            Artefacts = bootstrapper.Get<IArtefactService>();
            Builds = bootstrapper.Get<IBuildService>();
            QueuedBuilds = bootstrapper.Get<IQueuedBuildService>();
            Changes = bootstrapper.Get<IChangeService>();
        }
    }
}
