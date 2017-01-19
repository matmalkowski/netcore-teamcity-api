using System.Collections.Generic;

namespace NetCoreTeamCity.Models
{
    internal class Builds
    {
        public int Count { get; set; }

        public IList<Build> Build { get; set; }
    }
}
