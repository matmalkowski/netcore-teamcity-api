using System.Collections.Generic;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    internal class BuildService : BaseBuildService, IBuildService
    {
        private readonly IBuildPinningService _buildPinningService;
        private const string Endpoint = "builds";

        public BuildService(ITeamCityApiClient apiClient, IBuildTagsService tagsService, IBuildPinningService buildPinningService) : base(apiClient)
        {
            _buildPinningService = buildPinningService;
            Tags = tagsService;
        }

        public Build Get(long buildId)
        {
            return Get(Endpoint, buildId);
        }

        public IList<Build> Find(BuildLocator locator, BuildField fields = null, int count = 100)
        {
            var query = GetQuery(locator, fields, count);
            return Find(Endpoint, query);
        }

        public IList<Build> Find(BuildField fields = null, int count = 100)
        {
            var query = GetQuery(null, fields, count);
            return Find(Endpoint, query);
        }

        public Build Stop(long buildId, string comment, bool reAddToTheQueue = false)
        {
            return CancelBuild(Endpoint, buildId, comment, reAddToTheQueue);
        }

        public IBuildTagsService Tags { get; }

        public void Pin(long buildId, string comment = null)
        {
            _buildPinningService.Pin(buildId);
        }

        public void UnPin(long buildId, string comment = null)
        {
            _buildPinningService.UnPin(buildId);
        }

        public bool IsPinned(long buildId)
        {
            return _buildPinningService.IsPinned(buildId);
        }
    }
}
