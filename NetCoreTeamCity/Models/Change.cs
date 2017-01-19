using System;

namespace NetCoreTeamCity.Models
{
    public class Change
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
    }
}
