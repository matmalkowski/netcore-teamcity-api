namespace NetCoreTeamCity.Clients
{
    internal interface IHttpClientWrapperFactory
    {
        IHttpClientWrapper Create();
    }
}