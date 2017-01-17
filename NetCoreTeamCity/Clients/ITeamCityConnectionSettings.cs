namespace NetCoreTeamCity.Clients
{
    internal interface ITeamCityConnectionSettings
    {
        string TeamCityHost { get; }
        string Username { get; }
        string Password { get; }
    }
}