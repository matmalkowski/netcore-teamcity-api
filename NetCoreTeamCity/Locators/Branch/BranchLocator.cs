using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NetCoreTeamCity.Locators.Branch
{
    public class BranchLocator : ILocator
    {
        private readonly List<ApiLocator> _locators;

        internal BranchLocator()
        {
            _locators = new List<ApiLocator>();
        }

        public BranchLocator Name(string name)
        {
            _locators.Add(new ApiLocator("name", WebUtility.UrlEncode(name)));
            return this;
        }

        public BranchLocator Default(Flag flag)
        {
            _locators.Add(new ApiLocator("default", flag.ToString().ToLower()));
            return this;
        }

        public BranchLocator Unspecified(Flag flag)
        {
            _locators.Add(new ApiLocator("unspecified", flag.ToString().ToLower()));
            return this;
        }

        public BranchLocator Branched(Flag flag)
        {
            _locators.Add(new ApiLocator("branched", flag.ToString().ToLower()));
            return this;
        }

        string ILocator.GetLocatorQueryString()
        {
            return string.Join(",", _locators.Select(l => l.Value));
        }
    }
}
