using System.Collections.Generic;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    public interface IArtefactService{
        IList<Artifact> Find(BuildLocator locator, int count = 100);
    }
}