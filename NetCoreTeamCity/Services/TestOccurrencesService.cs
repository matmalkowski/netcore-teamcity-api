using NetCoreTeamCity.Api;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Locators;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.TestOccurrences;
using NetCoreTeamCity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NetCoreTeamCity.Services
{
    internal class TestOccurrencesService : ITestOccurencesService
    {
        private readonly ITeamCityApiClient ApiClient;
        private string endpoint = "testOccurrences";

        public TestOccurrencesService(ITeamCityApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        public TestOccurrence Get(string id)
        {
            try
            {
                return ApiClient.Get<TestOccurrence>($"{endpoint}/{id}");
            }
            catch (HttpException exception)
            {
                if (exception.StatusCode == HttpStatusCode.NotFound) return null;
                throw;
            }
        }

        public IList<TestOccurrence> Find(BuildLocator locator, TestOccurrenceField fields = null, int count = 100, int start = 0, int lookuplimit = 0)
        {
            string query = GetQuery(By.TestOccurences.Build(locator), count, start, lookuplimit);
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            var testOccurreces = ApiClient.Get<TestOccurrences>($"{endpoint}{query}");
            return testOccurreces.TestOccurrenceItems == null ? new List<TestOccurrence>() : testOccurreces.TestOccurrenceItems;
        }

        public IList<TestOccurrence> Find(TestOccurrenceLocator locator, TestOccurrenceField fields = null, int count = 100, int start = 0, int lookuplimit = 0)
        {
            string query = GetQuery(locator, count, start, lookuplimit);
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            var testOccurreces = ApiClient.Get<TestOccurrences>($"{endpoint}{query}");
            return testOccurreces.TestOccurrenceItems == null ? new List<TestOccurrence>() : testOccurreces.TestOccurrenceItems;
        }

        private string GetQuery(ILocator locator, int count, int start, int lookuplimit)
        {
            var query = $"?locator=";
            if (count > 0) { query += $"count:{count},"; }
            if (start > 0) { query += $"start:{start},"; }
            if (lookuplimit > 0) { query += $"lookuplimit:{lookuplimit},"; }
            if (locator != null) query += $"{locator.GetLocatorQueryString()}";
            return query;
        }
    }
}
