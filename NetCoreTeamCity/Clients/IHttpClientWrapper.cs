using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreTeamCity.Clients
{
    internal interface IHttpClientWrapper
    {
        HttpResponseMessage Get(string url, string contentType = HttpContentType.Json);
        HttpResponseMessage Post(string url, HttpContent content, string contentType = HttpContentType.Json);
        Task<HttpResponseMessage> GetAsync(string url, string contentType = HttpContentType.Json);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string contentType = HttpContentType.Json);

        void SetBasicAuthentication(string userName, string password, bool forceBasicAuth);
    }
}