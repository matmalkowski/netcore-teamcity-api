using System.Collections.Generic;

namespace NetCoreTeamCity.Models
{
    public class TestOccurrences
    {
        public int Count { get; set; }
        public string Href { get; set; }
        public bool Default { get; set; }
        public int Passed { get; set; }
        public int Failed { get; set; }
        public int NewFailed { get; set; }
        public int Ignored { get; set; }
        public string NextHref { get; set; }
        public string PrevHref { get; set; }
        public List<TestOccurrence> testOccurrenceItems { get; set; }

    }
}
