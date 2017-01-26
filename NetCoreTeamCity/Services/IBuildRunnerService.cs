using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IBuildRunnerService
    {
        Build Run(string buildTypeId, string branchName = null, string comment = null);
        Build Run(string buildTypeId, string branchName = null, BuildRunOptions options = null);
    }
}