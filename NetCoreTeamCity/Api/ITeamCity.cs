using NetCoreTeamCity.Models;
using NetCoreTeamCity.Services;

namespace NetCoreTeamCity.Api
{
    public interface ITeamCity
    {
        IBuildService Builds { get; }
    }
}