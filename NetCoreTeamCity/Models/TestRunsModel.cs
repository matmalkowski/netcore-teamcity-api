using System.Collections.Generic;

namespace NetCoreTeamCity.Models
{
    internal class TestRunsModel
    {
        public int Count { get; set; }
        public string Href { get; set; }
        public string NextHref { get; set; }
        public string PrevHref { get; set; }
        public List<TestOccurrence> TestOccurrences { get; set; }

    }
}
