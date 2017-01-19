using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace NetCoreTeamCity.Models
{
    

    

    public class Build
    {
        public long Id { get; set; }
        public string Number { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BuildStatus? Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BuildState? State { get; set; }

        public string BuildTypeId { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
        public string StatusText { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime QueuedDate { get; set; }
        public BuildType BuildType { get; set; }
        public Triggered Triggered { get; set; }
        public LastChanges LastChanges { get; set; }
        public Revisions Revisions { get; set; }
        public Agent Agent { get; set; }
        public Properties Properties { get; set; }
    }
}
