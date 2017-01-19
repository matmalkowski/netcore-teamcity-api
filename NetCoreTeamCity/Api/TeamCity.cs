using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;

namespace NetCoreTeamCity.Api
{
    public class TeamCity : ITeamCity
    {
        private readonly IBuildService _buildService;
        public TeamCity(string host, string userName, string password, bool usingSSL = true)
        {
            var connectionConfig = new TeamCityConnectionSettingsBuilder().ToHost(host).UsingSSL().AsUser(userName, password).Build();
            var bootstrapper = new BootStrapper(connectionConfig);
            _buildService = bootstrapper.Get<IBuildService>();
        }

        public IBuildService Builds { get { return _buildService; } }
    }
}
