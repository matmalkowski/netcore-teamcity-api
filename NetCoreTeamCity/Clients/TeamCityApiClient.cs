using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetCoreTeamCity.Clients
{
    internal class TeamCityApiClient : ITeamCityApiClient
    {
        private const string AcceptPlainText = "text/plain";
        private readonly ITeamCityConnectionSettings _teamCityConnectionSettings;
        private readonly IHttpClientWrapperFactory _httpClientWrapperFactory;

        public TeamCityApiClient(ITeamCityConnectionSettings teamCityConnectionSettings, IHttpClientWrapperFactory httpClientWrapperFactory)
        {
            _teamCityConnectionSettings = teamCityConnectionSettings;
            _httpClientWrapperFactory = httpClientWrapperFactory;
        }

        public T Get<T>(string url)
        {
            using (var client = GetHttpClient())
            {
                var response = client.Get(GetRequestUri(url), RequestContentType(typeof(T)));
                if (!response.IsSuccessStatusCode)
                {
                    ThrowHttpException(response, GetRequestUri(url));
                }

                if (_teamCityConnectionSettings.FavorJsonOverXml)
                {
                    var jsonStringContent = response.Content.ReadAsStringAsync().Result;
                    if (typeof(T) == typeof(string)) return (T)Convert.ChangeType(jsonStringContent, typeof(T));
                    return JsonConvert.DeserializeObject<T>(jsonStringContent, new TeamCityDateTimeConventer());
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public T Post<T>(string url, T obj)
        {
            return Post<T, T>(url, obj);
        }

        public T2 Post<T1, T2>(string url, T1 obj)
        {
            using (var client = GetHttpClient())
            {
                return MakeTwoWayRequest<T1, T2>(url, obj, client.Post);
            }
        }

        public T Put<T>(string url, T obj)
        {
            return Put<T, T>(url, obj);
        }

        public T2 Put<T1, T2>(string url, T1 obj)
        {
            using (var client = GetHttpClient())
            {
                return MakeTwoWayRequest<T1, T2>(url, obj, client.Put);
            }
        }

        public void Delete<T>(string url, T obj)
        {
            using (var client = GetHttpClient())
            {
                string content = string.Empty;
                if (obj != null)
                {
                    if (_teamCityConnectionSettings.FavorJsonOverXml)
                    {
                        content = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            DateFormatString = TeamCityDateTimeFormat.DateTimeFormat
                        });
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }

                var response = client.Delete(GetRequestUri(url), new StringContent(content, Encoding.UTF8, RequestContentType(typeof(T))), RequestContentType(typeof(T)));
                if (!response.IsSuccessStatusCode)
                {
                    ThrowHttpException(response, GetRequestUri(url));
                }
            }
        }

        public async Task DownloadAsync(string url, string pathTo)
        {
            using var client = GetHttpClient();
            await client.DownloadAsync(GetRequestUri(url), pathTo);
        }

        private T2 MakeTwoWayRequest<T1, T2>(string url, T1 obj, Func<string, HttpContent, string, HttpResponseMessage> httpClientMethod)
        {
            string content;
            if (_teamCityConnectionSettings.FavorJsonOverXml)
            {
                content = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateFormatString = TeamCityDateTimeFormat.DateTimeFormat
                });
            }
            else
            {
                throw new NotImplementedException();
            }
            var response = httpClientMethod.Invoke(GetRequestUri(url), new StringContent(content, Encoding.UTF8, RequestContentType(typeof(T1))), RequestContentType(typeof(T2)));

            if (!response.IsSuccessStatusCode)
            {
                ThrowHttpException(response, GetRequestUri(url));
            }

            if (_teamCityConnectionSettings.FavorJsonOverXml)
            {
                var jsonStringContent = response.Content.ReadAsStringAsync().Result;
                if (typeof(T2) == typeof(string)) return (T2)Convert.ChangeType(jsonStringContent, typeof(T2));
                return JsonConvert.DeserializeObject<T2>(jsonStringContent, new TeamCityDateTimeConventer());
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private string RequestContentType(Type type)
        {
            if (type == typeof(string)) return AcceptPlainText;
            return _teamCityConnectionSettings.FavorJsonOverXml? HttpContentType.Json: HttpContentType.Xml;
        } 

        private string GetRequestUri(string url)
        {
            return new Uri(_teamCityConnectionSettings.TeamCityHost, $"{Auth}app/rest/{url}").ToString();
        }

        private string Auth
        {
            get
            {
                if (string.IsNullOrEmpty(_teamCityConnectionSettings.Token))

                {
                    return _teamCityConnectionSettings.ConnectAsGuest ? "guestAuth/" : "httpAuth/";
                }
                
                return String.Empty;
            }
        }
        
        private static void ThrowHttpException(HttpResponseMessage response, string url)
        {
            throw new HttpException(response.StatusCode, $"Error: {response.StatusCode}\nHTTP: {response.StatusCode}\nURL: {url}\n{response.Content.ReadAsStringAsync().Result}");
        }

        private IHttpClientWrapper GetHttpClient()
        {
            var httpClient = _httpClientWrapperFactory.Create();

            if (!string.IsNullOrEmpty(_teamCityConnectionSettings.Token))
            {
                httpClient.SetBearerTokenAuthentication(_teamCityConnectionSettings.Token);
                return httpClient;
            }

            if (!_teamCityConnectionSettings.ConnectAsGuest)
            {
                if (string.IsNullOrEmpty(_teamCityConnectionSettings.Username) || string.IsNullOrEmpty(_teamCityConnectionSettings.Password))
                    throw new ArgumentException("When connecting as guest you must specify username and password");
                httpClient.SetBasicAuthentication(_teamCityConnectionSettings.Username, _teamCityConnectionSettings.Password);
            }
            return httpClient;
        }
    }
}
