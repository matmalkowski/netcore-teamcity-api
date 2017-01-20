using NetCoreTeamCity.Locators.Branch;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.BuildConfiguration;
using NetCoreTeamCity.Locators.User;

namespace NetCoreTeamCity.Api
{
    public static class By
    {
        public static BuildLocator Build => new BuildLocator();
        public static BuildConfigurationLocator BuildType => new BuildConfigurationLocator();
        public static BranchLocator Branch => new BranchLocator();
        public static UserLocator User => new UserLocator();
    }
}
