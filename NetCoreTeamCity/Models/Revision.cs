namespace NetCoreTeamCity.Models
{
    public class Revision
    {
        public string Version { get; set; }
        public string VcsBranchName { get; set; }
        public VcsRootInstance VcsRootInstance { get; set; }
    }
}
