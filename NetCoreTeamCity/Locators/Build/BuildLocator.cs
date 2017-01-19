using NetCoreTeamCity.Locators;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreTeamCity.ApiParameters.Build
{
    public class BuildLocator
    {
        private readonly List<ApiLocator> _locators;

        internal BuildLocator()
        {
            _locators = new List<ApiLocator>();
        }

        public BuildLocator Id(long buildId)
        {
            _locators.Add(new ApiLocator("id", buildId.ToString()));
            return this;
        }

        internal string GetLocatorQueryString()
        {
            return string.Join(",", _locators.Select(l => l.Value));
        }
    }
}
