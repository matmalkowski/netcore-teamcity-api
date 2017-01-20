using System.Collections.Generic;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IBuildService
    {
        Build Get(long buildId);
        IList<Build> Find(BuildLocator locator, BuildField fields);

    }
}