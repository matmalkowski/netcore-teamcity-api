using System.Collections.Generic;

namespace NetCoreTeamCity.Models
{
    public class Revisions
    {
        public int Count { get; set; }
        public IList<Revision> Revision { get; set; }
    }
}
