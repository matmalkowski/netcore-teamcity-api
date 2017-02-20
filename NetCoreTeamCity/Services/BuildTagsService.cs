using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreTeamCity.Locators.Build;

namespace NetCoreTeamCity.Services
{
    public class BuildTagsService : IBuildTagsService
    {
        public IList<string> Get(BuildLocator locator)
        {
            throw new NotImplementedException();
        }

        public IList<string> Replace(BuildLocator locator, string tag)
        {
            throw new NotImplementedException();
        }

        public IList<string> Replace(BuildLocator locator, IList<string> tags)
        {
            throw new NotImplementedException();
        }

        public IList<string> Add(BuildLocator locator, string tag)
        {
            throw new NotImplementedException();
        }

        public IList<string> Add(BuildLocator locator, IList<string> tags)
        {
            throw new NotImplementedException();
        }
    }
}
