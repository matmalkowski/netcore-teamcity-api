using System.Collections.Generic;

namespace NetCoreTeamCity.Locators.TestOccurrences
{
    public class TestOccurrenceField
    {
        private readonly List<string> _fields;

        internal TestOccurrenceField()
        {
            _fields = new List<string>();
        }
        public TestOccurrenceField Default()
        {
            Id().Name().Status().Duration().Href();
            return this;
        }

        public TestOccurrenceField Id()
        {
            _fields.Add("id");
            return this;
        }

        public TestOccurrenceField Name()
        {
            _fields.Add("name");
            return this;
        }

        public TestOccurrenceField Status()
        {
            _fields.Add("status");
            return this;
        }

        public TestOccurrenceField Ignored()
        {
            _fields.Add("ignored");
            return this;
        }
        public TestOccurrenceField Duration()
        {
            _fields.Add("duration");
            return this;
        }
        public TestOccurrenceField RunOrder()
        {
            _fields.Add("runOrder");
            return this;
        }
        public TestOccurrenceField Muted()
        {
            _fields.Add("muted");
            return this;
        }
        public TestOccurrenceField CurrentlyMuted()
        {
            _fields.Add("currentlyMuted");
            return this;
        }
        public TestOccurrenceField CurrentlyInvestigated()
        {
            _fields.Add("currentlyInvestigated");
            return this;
        }
        public TestOccurrenceField Href()
        {
            _fields.Add("href");
            return this;
        }
        public TestOccurrenceField Details()
        {
            _fields.Add("details");
            return this;
        }
        public TestOccurrenceField Test()
        {
            _fields.Add("test");
            return this;
        }
        public TestOccurrenceField Build()
        {
            _fields.Add("build");
            return this;
        }

        internal string GetFieldsQueryString()
        {
            if(_fields.Count == 0) { Default(); }
            return $"testOccurrence({string.Join(",", _fields)})";
        }
    }
}
