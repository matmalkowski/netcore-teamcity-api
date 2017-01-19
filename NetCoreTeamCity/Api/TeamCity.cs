using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Api
{
    public class TeamCity : ITeamCity
    {
        private ITeamCityApiClient _teamCityService;
        public TeamCity(string host, string userName, string password, bool usingSSL = true)
        {
            var connectionConfig = new TeamCityConnectionSettingsBuilder().ToHost(host).UsingSSL().AsUser(userName, password).Build();
            var bootstrapper = new BootStrapper(connectionConfig);
            _teamCityService = bootstrapper.Get<ITeamCityApiClient>();

        }

        public Build GetBuild()
        {
            return _teamCityService.Get<Build>("builds/id:412910");
        }
    }
}
