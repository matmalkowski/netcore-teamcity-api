using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreTeamCity.ApiParameters.Build;

namespace NetCoreTeamCity.Api
{
    public static class Include
    {
        public static BuildField Build => new BuildField();
    }
}
