using System;

namespace NetCoreTeamCity.Models
{
    public class TestOccurrence
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public TestRunStatus Status { get; set; }
        public bool? Ignored { get; set; }
        public int? Duration { get; set; }
        public int? RunOrder { get; set; }
        public bool? Muted { get; set; }
        public bool? CurrentlyMuted { get; set; }
        public bool? currentlyInvestigated { get; set; }
        public Test Test { get; set; }
        public Build Build { get; set; }
    }

    public enum TestRunStatus
    {
        SUCCESS,
        FAILURE,
        UNKNOWN
    }
}
