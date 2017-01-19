using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IBuildService
    {
        Build Get(long buildId);
    }
}