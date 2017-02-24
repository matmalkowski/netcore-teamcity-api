using System.Collections.Generic;

namespace NetCoreTeamCity.Services
{
    public interface IBuildTagsService
    {
        IList<string> Get(long buildId);
        IList<string> Replace(long buildId, string tag);
        IList<string> Replace(long buildId, IList<string> tags);
        IList<string> Add(long buildId, string tag);
        IList<string> Add(long buildId, IList<string> tags);
    }
}