using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    internal class BuildRunnerService : BaseBuildService, IBuildRunnerService
    {
        private const string Endpoint = "buildQueue";

        public BuildRunnerService(ITeamCityApiClient apiClient) : base(apiClient) { }

        public Build Run(string buildTypeId, string branchName = null, string comment = null)
        {
            throw new NotImplementedException();
        }

        public Build Run(string buildTypeId, string branchName = null, BuildRunOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
