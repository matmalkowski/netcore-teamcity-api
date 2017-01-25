using System;
using System.Collections.Generic;

namespace NetCoreTeamCity.Models
{
    public class Build
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public BuildStatus? Status { get; set; }
        public BuildState? State { get; set; }
        public string BuildTypeId { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
        public string StatusText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? QueuedDate { get; set; }
        public BuildConfiguration BuildType { get; set; }
        public Triggered Triggered { get; set; }
        public IList<Change> LastChanges { get; set; }
        public IList<Revision> Revisions { get; set; }
        public Agent Agent { get; set; }
        public IList<Property> Properties { get; set; }
        public TestOccurrences TestOccurrences { get; set; }
    }
}
