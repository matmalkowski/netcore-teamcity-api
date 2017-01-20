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

        public IList<Build> Find(BuildLocator locator, BuildField fields = null)
        {
            var query = $"builds?locator={locator.GetLocatorQueryString()}";
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            
            var builds = _apiClient.Get<Builds>(query);
            return builds == null ? new List<Build>() : builds.Build.Select(b => b.Convert()).ToList();
        }
    }
}
