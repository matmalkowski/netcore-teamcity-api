namespace NetCoreTeamCity.Models
{
    internal class BuildCancelRequest
    {
        public string Comment { get; set; }
        public bool ReAddIntoQueue { get; set; }
    }
}
