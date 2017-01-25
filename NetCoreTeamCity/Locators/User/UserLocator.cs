using System;

namespace NetCoreTeamCity.Locators.User
{
    public class UserLocator : ILocator
    {
        private ApiLocator _locator;

        internal UserLocator() { }

        public UserLocator Id(long id)
        {
            _locator = new ApiLocator("id", id.ToString());
            return this;
        }

        public UserLocator Name(string name)
        {
            _locator = new ApiLocator("name", name);
            return this;
        }

        string ILocator.GetLocatorQueryString()
        {
            return _locator.Value;
        }
    }
}
