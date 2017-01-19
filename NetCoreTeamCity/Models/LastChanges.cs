using System.Collections.Generic;

namespace NetCoreTeamCity.Models
{
    public class LastChanges
    {
        public int Count { get; set; }
        public IList<Change> Change { get; set; }
    }
}
