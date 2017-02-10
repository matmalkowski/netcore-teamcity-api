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
        protected readonly ITeamCityApiClient ApiClient;

        protected BaseBuildService(ITeamCityApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        protected Build Get(string endpoint, long id)
        {
            try
            {
                return ApiClient.Get<BuildModel>($"{endpoint}/id:{id}").Convert();
            }
            catch (HttpException exception)
            {
                if (exception.StatusCode == HttpStatusCode.NotFound) return null;
                throw;
            }
        }

        protected IList<Build> Find(string endpoint, string query)
        {
            var builds = ApiClient.Get<Builds>($"{endpoint}{query}");
            return builds.Build == null ? new List<Build>() : builds.Build.Select(b => b.Convert()).ToList();
        }

        protected Build AddToQueue(string endpoint, BuildModel buildModel)
        {
            return ApiClient.Post(endpoint, buildModel).Convert();
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
