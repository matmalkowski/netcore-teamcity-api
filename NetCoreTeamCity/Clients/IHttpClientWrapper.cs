using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreTeamCity.Clients
{
    internal interface IHttpClientWrapper : IDisposable
    {
        HttpResponseMessage Get(string url, string contentType = HttpContentType.Json);
        HttpResponseMessage Post(string url, HttpContent content, string contentType = HttpContentType.Json);
        HttpResponseMessage Put(string url, HttpContent content, string contentType = HttpContentType.Json);
        HttpResponseMessage Delete(string url, HttpContent content, string contentType = HttpContentType.Json);


        Task<HttpResponseMessage> GetAsync(string url, string contentType = HttpContentType.Json);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string contentType = HttpContentType.Json);
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content, string contentType = HttpContentType.Json);
        Task<HttpResponseMessage> DeleteAsync(string url, HttpContent content, string contentType = HttpContentType.Json);

        void SetBasicAuthentication(string userName, string password);

        void SetBearerTokenAuthentication(string token);
    }
}