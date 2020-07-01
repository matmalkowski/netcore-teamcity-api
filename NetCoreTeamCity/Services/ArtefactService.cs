using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IList<Artifact> Find(BuildLocator locator, int count = 10)
        {
            var query = GetQuery(locator, count);
            return Find(Endpoint, query);
        }

        public async Task DownloadAsync(BuildLocator locator, string path, int count = 10)
        {
            var artifacts = Find(locator, count);

            foreach (var artifact in artifacts)
            {
                await ApiClient.DownloadAsync(artifact.Href, $"{path}/{artifact.Name}");
            }
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