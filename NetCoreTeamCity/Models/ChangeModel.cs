using System;

namespace NetCoreTeamCity.Models
{
    internal class ChangeModel
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Username { get; set; }
        public DateTime? Date { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
        public Files Files { get; set; }
        public VcsRootInstance VcsRootInstance { get; set; }
    }
}
