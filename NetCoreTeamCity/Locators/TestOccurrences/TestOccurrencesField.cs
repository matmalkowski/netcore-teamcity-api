using NetCoreTeamCity.Api;
using System.Collections.Generic;

namespace NetCoreTeamCity.Locators.TestOccurrences
{
    public class TestOccurrencesField
    {
        private readonly List<string> _fields;

        internal TestOccurrencesField()
        {
            _fields = new List<string>() {  };
        }

        public TestOccurrencesField Count()
        {
            _fields.Add("count");
            return this;
        }

        public TestOccurrencesField Href()
        {
            _fields.Add("href");
            return this;
        }

        public TestOccurrencesField TestOccurrence(TestOccurrenceField testOccurenceField)
        {
            _fields.Add(testOccurenceField.GetFieldsQueryString());
            return this;
        }

        internal string GetFieldsQueryString()
        {
            if(_fields.Count == 0) { Count().Href().TestOccurrence(Include.TestOccurrenceField); }
            return string.Join(",", _fields);
        }
    }
}
