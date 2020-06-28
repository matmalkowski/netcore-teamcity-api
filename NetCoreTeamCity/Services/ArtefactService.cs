using System.Collections.Generic;
using System.Linq;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Locators;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    internal class ArtefactService : IArtefactService
    {
        private ITeamCityApiClient ApiClient { get; }
        private const string Endpoint = "builds";
        
        public ArtefactService(ITeamCityApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        public IList<Artifact> Find(BuildLocator locator, int count = 100)
        {
            var query = GetQuery(locator, count);
            return Find(Endpoint, query);
        }
        
        private IList<Artifact> Find(string endpoint, string query)
        {
            var artifacts = ApiClient.Get<Artifacts>($"{endpoint}{query}/artifacts");
            return artifacts.File == null ? new List<Artifact>() : artifacts.File.Select(b => b.Convert()).ToList();
        }
        
        private string GetQuery(ILocator locator, int count = 100)
        {
            var query = $"/count:{count}";
            if (locator != null) query += $",{locator.GetLocatorQueryString()}";
            return query;
        }
    }
}