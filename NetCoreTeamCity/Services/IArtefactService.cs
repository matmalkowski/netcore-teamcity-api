using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IArtefactService
    {
        IList<Artifact> Find(BuildLocator locator, int count = 10);

        Task DownloadAsync(BuildLocator locator, string path, int count = 10);
    }
}