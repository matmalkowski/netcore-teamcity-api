namespace NetCoreTeamCity.Clients
{
    internal interface ITeamCityApiClient
    {
        T Get<T>(string url);
    }
}