using System.Collections.Generic;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IBuildService
    {
        Build Get(long buildId);
        IList<Build> Find(BuildLocator locator, BuildField fields = null, int count = 100);
        IList<Build> Find(BuildField fields = null, int count = 100);
        Build Stop(long buildId, string comment, bool reAddToTheQueue = false);
        IBuildTagsService Tags { get; }
    }
}