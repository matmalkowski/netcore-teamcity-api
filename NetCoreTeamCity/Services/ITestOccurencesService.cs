using System.Collections.Generic;
using NetCoreTeamCity.Locators.TestOccurrences;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface ITestOccurencesService
    {
        TestOccurrence Get(string testOccurenceId);
        IList<TestOccurrence> Find(TestOccurrencesLocator locator, TestOccurrenceField fields = null);
    }
}