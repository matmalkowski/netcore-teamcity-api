using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.BuildConfiguration;

namespace NetCoreTeamCity.Api
{
    public static class By
    {
        public static BuildLocator Build => new BuildLocator();
        public static BuildConfigurationLocator BuildType => new BuildConfigurationLocator();

    }
}
