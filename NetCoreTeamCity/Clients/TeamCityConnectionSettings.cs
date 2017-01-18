
namespace NetCoreTeamCity.Clients
{
    internal class TeamCityConnectionSettings : ITeamCityConnectionSettings
    {
        private readonly string _teamCityHost;
        private readonly string _userName;
        private readonly string _password;

        public TeamCityConnectionSettings(string teamCityHost, string userName, string password)
        {
            _teamCityHost = teamCityHost;
            _userName = userName;
            _password = password;
        }
        public string TeamCityHost { get { return _teamCityHost; } }
        public string Username { get { return _userName; } }
        public string Password { get { return _password; } }
    }
}
