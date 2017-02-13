namespace NetCoreTeamCity.Clients
{
    internal interface ITeamCityApiClient
    {
        T Get<T>(string url);
        T Post<T>(string url, T obj);
        T2 Post<T1, T2>(string url, T1 obj);
    }
}