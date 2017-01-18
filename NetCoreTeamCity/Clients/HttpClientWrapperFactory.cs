namespace NetCoreTeamCity.Clients
{
    internal class HttpClientWrapperFactory : IHttpClientWrapperFactory
    {
        public IHttpClientWrapper Create()
        {
            return new HttpClientWrapper();
        }
    }
}
