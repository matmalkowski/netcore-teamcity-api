namespace NetCoreTeamCity.Clients
{
    internal class TeamCityConnectionSettingsBuilder
    {
        private string _teamCityHost;
        private string _userName;
        private string _password;

        public TeamCityConnectionSettingsBuilder()
        {
            _teamCityHost = "localhost";
            _userName = "guest";
        }

        public TeamCityConnectionSettingsBuilder ToHost(string host)
        {
            _teamCityHost = host;
            return this;
        }

        public TeamCityConnectionSettingsBuilder AsUser(string userName, string password)
        {
            _userName = userName;
            _password = password;
            return this;
        }

        public ITeamCityConnectionSettings Build()
        {
            return new TeamCityConnectionSettings(_teamCityHost, _userName, _password);
        }
    }
}
