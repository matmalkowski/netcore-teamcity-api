namespace NetCoreTeamCity.Clients
{
    internal interface ITeamCityApiClient
    {
        T Get<T>(string url);
        T Post<T>(string url, T obj);
    }
}