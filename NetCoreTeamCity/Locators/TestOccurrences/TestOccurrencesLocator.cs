using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetCoreTeamCity.Helpers;
using NetCoreTeamCity.Locators.Branch;
using NetCoreTeamCity.Locators.BuildConfiguration;
using NetCoreTeamCity.Locators.User;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.Project;

namespace NetCoreTeamCity.Locators.TestOccurrences
{
    public class TestOccurrencesLocator : ILocator
    {
        private readonly List<ApiLocator> _locators;

        internal TestOccurrencesLocator()
        {
            _locators = new List<ApiLocator>();
        }

        public TestOccurrencesLocator Id(long buildId)
        {
            _locators.Add(new ApiLocator("id", buildId.ToString()));
            return this;
        }

        public TestOccurrencesLocator Test(string testName)
        {
            _locators.Add(new ApiLocator("test", testName));
            return this;
        }

        public TestOccurrencesLocator Build(BuildLocator build)
        {
            _locators.Add(new ApiLocator((build as ILocator).GetLocatorQueryString()));
            return this;
        }

        public TestOccurrencesLocator BuildType(BuildConfigurationLocator buildTypeLocator)
        {
            _locators.Add(new ApiLocator((buildTypeLocator as ILocator).GetLocatorQueryString()));
            return this;
        }

        public TestOccurrencesLocator AffectedProject(ProjectLocator affectedProject)
        {
            _locators.Add(new ApiLocator((affectedProject as ILocator).GetLocatorQueryString()));
            return this;
        }

        public TestOccurrencesLocator CurrentlyFailing(bool currentlyFailing)
        {
            _locators.Add(new ApiLocator("currentlyFailing", currentlyFailing.ToString()));
            return this;
        }

        public TestOccurrencesLocator Branch(string branchName)
        {
            _locators.Add(new ApiLocator("branch", branchName));
            return this;
        }

        public TestOccurrencesLocator Ignored(bool ignored)
        {
            _locators.Add(new ApiLocator("ignored", ignored.ToString()));
            return this;
        }
        public TestOccurrencesLocator Muted(bool muted)
        {
            _locators.Add(new ApiLocator("muted", muted.ToString()));
            return this;
        }
        public TestOccurrencesLocator CurrentlyMuted(bool currentlyMuted)
        {
            _locators.Add(new ApiLocator("currentlyMuted", currentlyMuted.ToString()));
            return this;
        }

        public TestOccurrencesLocator CurrentlyInvestigated(bool currentlyInvestigated)
        {
            _locators.Add(new ApiLocator("currentlyInvestigated", currentlyInvestigated.ToString()));
            return this;
        }

        public TestOccurrencesLocator Start(int start)
        {
            _locators.Add(new ApiLocator("start", start.ToString()));
            return this;
        }

        public TestOccurrencesLocator Count(int count)
        {
            _locators.Add(new ApiLocator("count", count.ToString()));
            return this;
        }

        public TestOccurrencesLocator LookupLimit(int lookupLimit)
        {
            _locators.Add(new ApiLocator("lookupLimit", lookupLimit.ToString()));
            return this;
        }





        string ILocator.GetLocatorQueryString()
        {
            return string.Join(",", _locators.Select(l => l.Value));
        }
    }
}
