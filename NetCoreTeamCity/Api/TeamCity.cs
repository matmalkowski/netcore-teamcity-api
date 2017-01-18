using NetCoreTeamCity.Clients;

namespace NetCoreTeamCity.Api
{
    public class TeamCity : ITeamCity
    {
        public TeamCity(string host, string userName, string password)
        {
            var connectionConfig = new TeamCityConnectionSettingsBuilder().ToHost(host).AsUser(userName, password).Build();

        }
    }
}
