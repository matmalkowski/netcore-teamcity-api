using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Locators;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NetCoreTeamCity.Services
{
    internal abstract class BaseBuildService
    {
        private readonly ITeamCityApiClient _apiClient;

        protected BaseBuildService(ITeamCityApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        protected Build Get(string endpoint, long id)
        {
            try
            {
                return _apiClient.Get<BuildModel>($"{endpoint}/id:{id}").Convert();
            }
            catch (HttpException exception)
            {
                if (exception.StatusCode == HttpStatusCode.NotFound) return null;
                throw;
            }
        }
        protected IList<Build> Find(string endpoint, string query)
        {
            var builds = _apiClient.Get<Builds>($"{endpoint}{query}");
            return builds.Build == null ? new List<Build>() : builds.Build.Select(b => b.Convert()).ToList();
        }

        protected string GetQuery(ILocator locator, BuildField fields = null, int count = 100)
        {
            var query = $"?locator=count:{count}";
            if (locator != null) query += $",{locator.GetLocatorQueryString()}";
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            return query;
        }
    }
}
