using NetCoreTeamCity.Locators.Build;

namespace NetCoreTeamCity.Api
{
    public static class By
    {
        public static BuildLocator Build => new BuildLocator();
    }
}
