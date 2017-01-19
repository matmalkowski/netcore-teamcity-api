using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Api
{
    public interface ITeamCity
    {
        Build GetBuild();
    }
}