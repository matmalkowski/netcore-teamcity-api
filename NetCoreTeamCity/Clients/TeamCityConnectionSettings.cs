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

        public TeamCityConnectionSettings(Uri teamCityHost, string token, bool favorJsonOverXml = true)
        {
            TeamCityHost = teamCityHost;
            Token = token;
            FavorJsonOverXml = favorJsonOverXml;
        }
        
        public Uri TeamCityHost { get; }
        public string Token { get; }
        public bool ConnectAsGuest { get; }
        public string Username { get; }
        public string Password { get; }
        public bool FavorJsonOverXml { get; }
    }
}
