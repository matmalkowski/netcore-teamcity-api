using System;

namespace NetCoreTeamCity.Clients
{
    internal class TeamCityConnectionSettings : ITeamCityConnectionSettings
    {
        public TeamCityConnectionSettings(Uri teamCityHost, bool connectAsGuest, string userName, string password, bool favorJsonOverXml = true)
        {
            TeamCityHost = teamCityHost;
            ConnectAsGuest = connectAsGuest;
            Username = userName;
            Password = password;
            FavorJsonOverXml = favorJsonOverXml;
        }
        public Uri TeamCityHost { get; private set; }
        public bool ConnectAsGuest { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool FavorJsonOverXml { get; private set; }
    }
}
