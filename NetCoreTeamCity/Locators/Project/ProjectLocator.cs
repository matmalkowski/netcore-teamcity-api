using System;
using System.Net;

namespace NetCoreTeamCity.Locators.Project
{
    public class ProjectLocator : ILocator
    {
        private string _locator;
        internal ProjectLocator() { }

        public ProjectLocator Id(string buildTypeId)
        {
            _locator = new ApiLocator("id", buildTypeId).Value;
            return this;
        }

        public ProjectLocator Name(string buildConfigurationName)
        {
            _locator = new ApiLocator("name", WebUtility.UrlEncode(buildConfigurationName)).Value;
            return this;
        }

        string ILocator.GetLocatorQueryString()
        {
            return $"project:({_locator})";
        }
    }
}
