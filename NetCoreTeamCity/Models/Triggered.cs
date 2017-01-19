using System;

namespace NetCoreTeamCity.Models
{
    public class Triggered
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}
