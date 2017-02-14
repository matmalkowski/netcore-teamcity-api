using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTeamCity.Models
{
    public class BuildRunningInfo
    {
        public int PercentageComplete { get; set; }
        public int ElapsedSeconds { get; set; }
        public int EstimatedTotalSeconds { get; set; }
        public string CurrentStageText { get; set; }
        public bool Outdated { get; set; }
        public bool ProbablyHanging { get; set; }
    }
}
