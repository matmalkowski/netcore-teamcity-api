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
    internal class TestOccurrencesService
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

        public IList<TestOccurrence> Find(TestOccurrencesLocator locator, TestOccurrenceField fields = null)
        {
            string query = GetQuery(locator, fields);
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            var testOccurreces = ApiClient.Get<TestOccurrences>($"{endpoint}{query}");
            return testOccurreces.testOccurrenceItems == null ? new List<TestOccurrence>() : testOccurreces.testOccurrenceItems;
        }

        public IList<TestOccurrence> Find(TestOccurrencesLocator locator, TestOccurrencesField fields = null)
        {
            string query = GetQuery(locator);
            if (fields != null) query += $"&fields={fields.GetFieldsQueryString()}";
            var testOccurreces = ApiClient.Get<TestOccurrences>($"{endpoint}{query}");
            return testOccurreces.testOccurrenceItems == null ? new List<TestOccurrence>() : testOccurreces.testOccurrenceItems;
        }

        private string GetQuery(ILocator locator, TestOccurrenceField fields = null)
        {
            var query = $"?locator=";
            if (locator != null) query += $"{locator.GetLocatorQueryString()}";
            return query;
        }
    }
}
