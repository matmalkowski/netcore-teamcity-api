using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    internal class BuildService : IBuildService
    {
        private readonly ITeamCityApiClient _apiClient;

        public BuildService(ITeamCityApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Build Get(long buildId)
        {
            try
            {
                return _apiClient.Get<BuildModel>($"builds/id:{buildId}").Convert();
            }
            catch (HttpException exception)
            {
                if (exception.StatusCode == HttpStatusCode.NotFound) return null;
                throw;
            }
        }

        public IList<Build> Find(BuildLocator locator, BuildField fields = null, int count = 100)
        {
            var query = GetQuery("builds", locator, fields, count);
            var builds = _apiClient.Get<Builds>(query);
            return builds.Build == null ? new List<Build>() : builds.Build.Select(b => b.Convert()).ToList();
        }

        public IList<Build> Find(BuildField fields = null, int count = 100)
        {
            var query = GetQuery("builds", null, fields, count);
            var builds = _apiClient.Get<Builds>(query);
            return builds == null ? new List<Build>() : builds.Build.Select(b => b.Convert()).ToList();
        }

        private string GetQuery(string endpoint, BuildLocator locator, BuildField fields = null, int count = 100)
        {
            var query = $"{endpoint}?locator=count:{count}";
            if (locator != null) query += $",{locator.GetLocatorQueryString()}";
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            return query;
        }
    }
}
