using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetCoreTeamCity.Locators.BuildConfiguration;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Locators.Project;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Locators.TestOccurrences
{
    public class TestOccurrenceLocator : ILocator
    {
        private readonly List<ApiLocator> _locators;

        internal TestOccurrenceLocator()
        {
            _locators = new List<ApiLocator>();
        }

        public TestOccurrenceLocator Id(long buildId)
        {
            _locators.Add(new ApiLocator("id", buildId.ToString()));
            return this;
        }

        public TestOccurrenceLocator Test(string testName)
        {
            _locators.Add(new ApiLocator("test", testName));
            return this;
        }

        public TestOccurrenceLocator Build(BuildLocator build)
        {
            _locators.Add(new ApiLocator("build", $"({ (build as ILocator).GetLocatorQueryString() })"));
            return this;
        }

        public TestOccurrenceLocator AffectedProject(string projectname)
        {
            _locators.Add(new ApiLocator("affectedProduct", projectname));
            return this;
        }

        public TestOccurrenceLocator CurrentlyFailing(bool currentlyFailing)
        {
            _locators.Add(new ApiLocator("currentlyFailing", currentlyFailing.ToString()));
            return this;
        }

        public TestOccurrenceLocator Branch(string branchName)
        {
            _locators.Add(new ApiLocator("branch", branchName));
            return this;
        }

        public TestOccurrenceLocator Ignored(bool ignored)
        {
            _locators.Add(new ApiLocator("ignored", ignored.ToString()));
            return this;
        }
        public TestOccurrenceLocator Muted(bool muted)
        {
            _locators.Add(new ApiLocator("muted", muted.ToString()));
            return this;
        }
        public TestOccurrenceLocator CurrentlyMuted(bool currentlyMuted)
        {
            _locators.Add(new ApiLocator("currentlyMuted", currentlyMuted.ToString()));
            return this;
        }

        public TestOccurrenceLocator CurrentlyInvestigated(bool currentlyInvestigated)
        {
            _locators.Add(new ApiLocator("currentlyInvestigated", currentlyInvestigated.ToString()));
            return this;
        }
        public TestOccurrenceLocator Status(TestRunStatus status)
        {
            _locators.Add(new ApiLocator("status", status.ToString()));
            return this;
        }

        string ILocator.GetLocatorQueryString()
        {
            return string.Join(",", _locators.Select(l => l.Value));
        }
    }
}
