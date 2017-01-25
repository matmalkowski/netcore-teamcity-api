using NetCoreTeamCity.Services;

namespace NetCoreTeamCity.Api
{
    public interface ITeamCity
    {
        IBuildService Builds { get; }
        IQueuedBuildService QueuedBuilds { get; }
    }
}