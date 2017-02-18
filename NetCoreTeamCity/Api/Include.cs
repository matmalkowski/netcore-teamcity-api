using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.TestOccurrences;

namespace NetCoreTeamCity.Api
{
    public static class Include
    {
        public static BuildField Build => new BuildField();
        public static TestOccurrenceField TestOccurrenceField => new TestOccurrenceField();
        public static TestOccurrencesField TestOccurrencesField => new TestOccurrencesField();
    }
}
