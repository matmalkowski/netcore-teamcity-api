using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTeamCity.Clients
{
    internal static class HttpContentType
    {
        internal const string Xml = "application/xml";
        internal const string Json = "application/json";
    }

    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        internal HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public HttpResponseMessage Get(string url, string contentType = HttpContentType.Json)
        {
            return GetAsync(url, contentType).Result;
        }

        public Task<HttpResponseMessage> GetAsync(string url, string contentType = HttpContentType.Json)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            return _client.GetAsync(url);
        }

        public HttpResponseMessage Post(string url, HttpContent content, string contentType = HttpContentType.Json)
        {
            return PostAsync(url, content, contentType).Result;
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string contentType = HttpContentType.Json)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            return _client.PostAsync(url, content);
        }

        public HttpResponseMessage Put(string url, HttpContent content, string contentType = HttpContentType.Json)
        {
            return PutAsync(url, content, contentType).Result;
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content, string contentType = HttpContentType.Json)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            return _client.PutAsync(url, content);
        }

        public HttpResponseMessage Delete(string url, HttpContent content, string contentType = HttpContentType.Json)
        {
            return DeleteAsync(url, content, contentType).Result;
        }

        public Task<HttpResponseMessage> DeleteAsync(string url, HttpContent content, string contentType = HttpContentType.Json)
        {
            var request = new HttpRequestMessage()
            {
                Content = content,
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            return _client.SendAsync(request);
        }

        public void SetBasicAuthentication(string userName, string password)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
        
        public void SetBearerTokenAuthentication(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
