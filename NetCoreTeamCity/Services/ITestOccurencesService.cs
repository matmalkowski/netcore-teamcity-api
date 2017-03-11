using System.Collections.Generic;
using NetCoreTeamCity.Locators.TestOccurrences;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Locators.Build;

namespace NetCoreTeamCity.Services
{
    public interface ITestOccurencesService
    {
        TestOccurrence Get(string testOccurenceId);
        IList<TestOccurrence> Find(BuildLocator locator, TestOccurrenceField fields = null, int count = 100, int start = 0, int lookuplimit = 0);
        IList<TestOccurrence> Find(TestOccurrenceLocator locator, TestOccurrenceField fields = null, int count = 100, int start = 0, int lookuplimit = 0);
    }
}