using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetCoreTeamCity.Locators.BuildConfiguration;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.Project;

namespace NetCoreTeamCity.Locators.TestOccurrences
{
    public class TestRunsLocator : ILocator
    {
        private readonly List<ApiLocator> _locators;

        internal TestRunsLocator()
        {
            _locators = new List<ApiLocator>();
        }

        public TestRunsLocator Id(long buildId)
        {
            _locators.Add(new ApiLocator("id", buildId.ToString()));
            return this;
        }

        public TestRunsLocator Test(string testName)
        {
            _locators.Add(new ApiLocator("test", testName));
            return this;
        }

        public TestRunsLocator Build(BuildLocator build)
        {
            _locators.Add(new ApiLocator("build", $"({ (build as ILocator).GetLocatorQueryString() })"));
            return this;
        }

        public TestRunsLocator BuildType(BuildConfigurationLocator buildTypeLocator)
        {
            _locators.Add(new ApiLocator("buildType", $"({(buildTypeLocator as ILocator).GetLocatorQueryString()})"));
            return this;
        }

        public TestRunsLocator AffectedProject(ProjectLocator affectedProject)
        {
            _locators.Add(new ApiLocator("affectedProduct", $"({(affectedProject as ILocator).GetLocatorQueryString()})"));
            return this;
        }

        public TestRunsLocator CurrentlyFailing(bool currentlyFailing)
        {
            _locators.Add(new ApiLocator("currentlyFailing", currentlyFailing.ToString()));
            return this;
        }

        public TestRunsLocator Branch(string branchName)
        {
            _locators.Add(new ApiLocator("branch", branchName));
            return this;
        }

        public TestRunsLocator Ignored(bool ignored)
        {
            _locators.Add(new ApiLocator("ignored", ignored.ToString()));
            return this;
        }
        public TestRunsLocator Muted(bool muted)
        {
            _locators.Add(new ApiLocator("muted", muted.ToString()));
            return this;
        }
        public TestRunsLocator CurrentlyMuted(bool currentlyMuted)
        {
            _locators.Add(new ApiLocator("currentlyMuted", currentlyMuted.ToString()));
            return this;
        }

        public TestRunsLocator CurrentlyInvestigated(bool currentlyInvestigated)
        {
            _locators.Add(new ApiLocator("currentlyInvestigated", currentlyInvestigated.ToString()));
            return this;
        }

        string ILocator.GetLocatorQueryString()
        {
            return string.Join(",", _locators.Select(l => l.Value));
        }
    }
}
