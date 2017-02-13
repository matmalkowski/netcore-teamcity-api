namespace NetCoreTeamCity.Models
{
    internal class BuildTriggeringOptions
    {
        public bool CleanSources { get; set; }
        public bool RebuildAllDependencies { get; set; }
        public bool QueueAtTop { get; set; }
    }
}
