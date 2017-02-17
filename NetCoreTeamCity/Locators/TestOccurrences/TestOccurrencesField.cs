using System.Collections.Generic;

namespace NetCoreTeamCity.Locators.TestOccurrences
{
    public class TestOccurrencesField
    {
        private readonly List<string> _fields;

        internal TestOccurrencesField()
        {
            _fields = new List<string>() { "count", "href", "nextHref", "prevHref", new TestOccurrenceField().GetFieldsQueryString() };
        }

        public TestOccurrencesField Count()
        {
            _fields.Add("count");
            return this;
        }

        public TestOccurrencesField Href()
        {
            _fields.Add("Href");
            return this;
        }
        public TestOccurrencesField NextHref()
        {
            _fields.Add("nextHref");
            return this;
        }
        public TestOccurrencesField PrevHref()
        {
            _fields.Add("PrevHref");
            return this;
        }

        internal string GetFieldsQueryString()
        {
            return string.Join(",", _fields);
        }
    }
}
