using System;

namespace NetCoreTeamCity.Clients
{
    internal class TeamCityConnectionSettingsBuilder
    {
        private string _teamCityHost;
        private bool _useSSL;
        private bool _useXml;
        private bool _guestMode;
        private string _userName;
        private string _password;
        private string _token;

        public TeamCityConnectionSettingsBuilder()
        {
            _teamCityHost = "localhost";
            _userName = "guest";
            _password = string.Empty;
        }

        public TeamCityConnectionSettingsBuilder ToHost(string host)
        {
            _teamCityHost = host;
            return this;
        }

        public TeamCityConnectionSettingsBuilder UsingSSL(bool usingSSL = true)
        {
            _useSSL = usingSSL;
            return this;
        }

        public TeamCityConnectionSettingsBuilder UsingXmlFormat()
        {
            _useXml = true;
            return this;
        }

        public TeamCityConnectionSettingsBuilder AsGuest()
        {
            _guestMode = true;
            _userName = "guest";
            _password = string.Empty;
            return this;
        }

        public TeamCityConnectionSettingsBuilder AsUser(string userName, string password)
        {
            _guestMode = false;
            _userName = userName;
            _password = password;
            return this;
        }

        public TeamCityConnectionSettingsBuilder WithToken(string token)
        {
            _token = token;
            return this;
        }

        public ITeamCityConnectionSettings Build()
        {
            var protocol = _useSSL ? "https" : "http";
            
            return string.IsNullOrEmpty(_token) ? 
                new TeamCityConnectionSettings(new Uri($"{protocol}://{_teamCityHost}"), _guestMode, _userName, _password, !_useXml) : 
                new TeamCityConnectionSettings(new Uri($"{protocol}://{_teamCityHost}"), _token, !_useXml);
        }
    }
}
