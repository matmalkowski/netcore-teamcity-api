using System;

namespace NetCoreTeamCity.Models
{
    public class BuildComment
    {
        public DateTime? Timestamp { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
    }
}
