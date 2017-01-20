using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetCoreTeamCity.Helpers;
using NetCoreTeamCity.Locators.Branch;
using NetCoreTeamCity.Locators.BuildConfiguration;
using NetCoreTeamCity.Locators.User;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Locators.Build
{
    public class BuildLocator
    {
        private readonly List<ApiLocator> _locators;

        internal BuildLocator()
        {
            _locators = new List<ApiLocator>();
        }

        public BuildLocator Id(long buildId)
        {
            _locators.Add(new ApiLocator("id", buildId.ToString()));
            return this;
        }

        public BuildLocator Number(long buildNumber)
        {
            _locators.Add(new ApiLocator("number", buildNumber.ToString()));
            return this;
        }

        public BuildLocator Project(string projectLocator)
        {
            _locators.Add(new ApiLocator("project", projectLocator));
            return this;
        }

        public BuildLocator AffectedProject(string projectLocator)
        {
            _locators.Add(new ApiLocator("affectedProject", projectLocator));
            return this;
        }

        public BuildLocator BuildType(BuildConfigurationLocator buildTypeLocator)
        {
            _locators.Add(new ApiLocator("buildType", $"({buildTypeLocator.GetLocatorQueryString()})"));
            return this;
        }

        public BuildLocator Tag(string tag)
        {
            return Tags(tag);
        }

        public BuildLocator Tags(params string[] tags)
        {
            foreach (var tag in tags)
            {
                _locators.Add(new ApiLocator("tag", tag));
            }
            return this;
        }

        public BuildLocator Status(BuildStatus buildStatus)
        {
            _locators.Add(new ApiLocator("status", buildStatus.ToString().ToUpper()));
            return this;
        }

        public BuildLocator User(UserLocator userLocator)
        {
            _locators.Add(new ApiLocator("user", $"({userLocator.GetLocatorQueryString()})"));
            return this;
        }

        public BuildLocator PersonalFlag(Flag flag)
        {
            _locators.Add(new ApiLocator("personal", flag.ToString().ToLower()));
            return this;
        }

        public BuildLocator CanceledFlag(Flag flag)
        {
            _locators.Add(new ApiLocator("canceled", flag.ToString().ToLower()));
            return this;
        }

        public BuildLocator FailedToStartFlag(Flag flag)
        {
            _locators.Add(new ApiLocator("failedToStart", flag.ToString().ToLower()));
            return this;
        }

        public BuildLocator State(BuildState buildState)
        {
            _locators.Add(new ApiLocator("state", buildState.ToString().ToUpper()));
            return this;
        }

        public BuildLocator RunningFlag(Flag flag)
        {
            _locators.Add(new ApiLocator("running", flag.ToString().ToLower()));
            return this;
        }

        public BuildLocator Hanging(bool flag = true)
        {
            if (flag)
            {
                State(BuildState.Running);
                _locators.Add(new ApiLocator("hanging", bool.TrueString));
            }
            return this;
        }

        public BuildLocator PinnedFlag(Flag flag)
        {
            _locators.Add(new ApiLocator("pinned", flag.ToString().ToLower()));
            return this;
        }

        public BuildLocator Branch(string branchName)
        {
            _locators.Add(new ApiLocator("branch", branchName));
            return this;
        }

        public BuildLocator Branch(BranchLocator branchLocator)
        {
            _locators.Add(new ApiLocator("branch", $"({branchLocator.GetLocatorQueryString()})"));
            return this;
        }

        public BuildLocator Revision(string revision)
        {
            _locators.Add(new ApiLocator("revision", revision));
            return this;
        }

        public BuildLocator AgentName(string agentName)
        {
            _locators.Add(new ApiLocator("agentName", agentName));
            return this;
        }

        public BuildLocator SinceBuild(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("sinceBuild", $"({buildLocator.GetLocatorQueryString()})"));
            return this;
        }

        public BuildLocator Since(DateTime date)
        {
            _locators.Add(new ApiLocator("sinceDate", WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))));
            return this;
        }

        public BuildLocator QueuedDateBefore(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("queuedDate", $"({buildLocator.GetLocatorQueryString()},condition:before)"));
            return this;
        }

        public BuildLocator QueuedDateAfter(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("queuedDate", $"({buildLocator.GetLocatorQueryString()},condition:after)"));
            return this;
        }

        public BuildLocator QueuedDateBefore(DateTime date)
        {
            _locators.Add(new ApiLocator("queuedDate", $"(date:{WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))},condition:before)"));
            return this;
        }

        public BuildLocator QueuedDateAfter(DateTime date)
        {
            _locators.Add(new ApiLocator("queuedDate", $"(date:{WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))},condition:after)"));
            return this;
        }

        public BuildLocator StartDateBefore(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("startDate", $"({buildLocator.GetLocatorQueryString()},condition:before)"));
            return this;
        }

        public BuildLocator StartDateAfter(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("startDate", $"({buildLocator.GetLocatorQueryString()},condition:after)"));
            return this;
        }

        public BuildLocator StartDateBefore(DateTime date)
        {
            _locators.Add(new ApiLocator("startDate", $"(date:{WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))},condition:before)"));
            return this;
        }

        public BuildLocator StartDateAfter(DateTime date)
        {
            _locators.Add(new ApiLocator("startDate", $"(date:{WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))},condition:after)"));
            return this;
        }
        
        public BuildLocator FinishDateBefore(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("finishDate", $"({buildLocator.GetLocatorQueryString()},condition:before)"));
            return this;
        }

        public BuildLocator FinishDateAfter(BuildLocator buildLocator)
        {
            _locators.Add(new ApiLocator("finishDate", $"({buildLocator.GetLocatorQueryString()},condition:after)"));
            return this;
        }

        public BuildLocator FinishDateBefore(DateTime date)
        {
            _locators.Add(new ApiLocator("finishDate", $"(date:{WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))},condition:before)"));
            return this;
        }

        public BuildLocator FinishDateAfter(DateTime date)
        {
            _locators.Add(new ApiLocator("finishDate", $"(date:{WebUtility.UrlEncode(date.ToString(TeamCityDateTimeFormat.DateTimeFormat))},condition:after)"));
            return this;
        }

        internal string GetLocatorQueryString()
        {
            return string.Join(",", _locators.Select(l => l.Value));
        }
    }
}
