using System;

namespace NetCoreTeamCity.Models
{
    public class Artifact
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public DateTime? ModificationTime { get; set; }
        public string Href { get; set; }
    }
}