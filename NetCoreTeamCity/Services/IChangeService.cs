using System.Collections.Generic;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IChangeService
    {
        Change Get(long id);
        IList<Change> Find();
    }
}