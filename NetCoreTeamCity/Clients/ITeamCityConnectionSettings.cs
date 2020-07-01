using System;

namespace NetCoreTeamCity.Clients
{
    public interface ITeamCityConnectionSettings
    {
        Uri TeamCityHost { get; }
        bool FavorJsonOverXml { get; }
        bool ConnectAsGuest { get; }
        string Username { get; }
        string Password { get; }
        string Token { get; }
    }
}