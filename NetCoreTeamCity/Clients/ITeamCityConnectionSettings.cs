using System;

namespace NetCoreTeamCity.Clients
{
    internal interface ITeamCityConnectionSettings
    {
        Uri TeamCityHost { get; }
        bool FavorJsonOverXml { get; }
        bool ConnectAsGuest { get; }
        string Username { get; }
        string Password { get; }
    }
}