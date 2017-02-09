using System;
using System.Net.Http;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Helpers;
using Newtonsoft.Json;

namespace NetCoreTeamCity.Clients
{
    internal class TeamCityApiClient : ITeamCityApiClient
    {
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
                var response = client.Get(GetRequestUri(url), RequestContentType);
                if (!response.IsSuccessStatusCode)
                {
                    ThrowHttpException(response, GetRequestUri(url));
                }

                if (_teamCityConnectionSettings.FavorJsonOverXml)
                {
                    var jsonStringConent = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(jsonStringConent, new TeamCityDateTimeConventer());
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public T Post<T>(string url, T obj)
        {
            using (var client = GetHttpClient())
            {
                string content;
                if (_teamCityConnectionSettings.FavorJsonOverXml)
                {
                    content = JsonConvert.SerializeObject(obj, new TeamCityDateTimeConventer());
                }
                else
                {
                    throw new NotImplementedException();
                }

                var response = client.Post(GetRequestUri(url), new StringContent(content));

                if (!response.IsSuccessStatusCode)
                {
                    ThrowHttpException(response, GetRequestUri(url));
                }

                if (_teamCityConnectionSettings.FavorJsonOverXml)
                {
                    var jsonStringConent = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(jsonStringConent, new TeamCityDateTimeConventer());
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private string RequestContentType => _teamCityConnectionSettings.FavorJsonOverXml ? HttpContentType.Json : HttpContentType.Xml;

        private string GetRequestUri(string url)
        {
            var auth = _teamCityConnectionSettings.ConnectAsGuest ? "guestAuth" : "httpAuth";
            return new Uri(_teamCityConnectionSettings.TeamCityHost, $"{auth}/app/rest/{url}").ToString();
        }

        private static void ThrowHttpException(HttpResponseMessage response, string url)
        {
            throw new HttpException(response.StatusCode, $"Error: {response.StatusCode}\nHTTP: {response.StatusCode}\nURL: {url}\n{response.Content.ReadAsStringAsync().Result}");
        }

        private IHttpClientWrapper GetHttpClient()
        {
            var httpClient = _httpClientWrapperFactory.Create();
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
