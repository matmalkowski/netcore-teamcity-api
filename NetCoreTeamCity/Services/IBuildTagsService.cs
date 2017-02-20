using System.Collections.Generic;
using NetCoreTeamCity.Locators.Build;

namespace NetCoreTeamCity.Services
{
    public interface IBuildTagsService
    {
        IList<string> Get(BuildLocator locator);
        IList<string> Replace(BuildLocator locator, string tag);
        IList<string> Replace(BuildLocator locator, IList<string> tags);
        IList<string> Add(BuildLocator locator, string tag);
        IList<string> Add(BuildLocator locator, IList<string> tags);
    }
}