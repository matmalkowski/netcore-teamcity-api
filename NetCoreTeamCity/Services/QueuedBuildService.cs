﻿using System.Collections.Generic;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;
using NetCoreTeamCity.Locators.BuildConfiguration;
using NetCoreTeamCity.Locators.Project;
using NetCoreTeamCity.Clients;
using System.Linq;

namespace NetCoreTeamCity.Services
{
    internal class QueuedBuildService : BaseBuildService, IQueuedBuildService
    {
        private const string Endpoint = "buildQueue";

        public QueuedBuildService(ITeamCityApiClient apiClient) : base(apiClient) { }

        public Build Get(long buildId)
        {
            return Get(Endpoint, buildId);
        }

        public IList<Build> Find(BuildField fields = null, int count = 100)
        {
            var query = GetQuery(null, fields, count);
            return Find(Endpoint, query);
        }

        public IList<Build> Find(BuildConfigurationLocator locator, BuildField fields = null, int count = 100)
        {
            var query = GetQuery(locator, fields, count);
            return Find(Endpoint, query);
        }

        public IList<Build> Find(ProjectLocator locator, BuildField fields = null, int count = 100)
        {
            var query = GetQuery(locator, fields, count);
            return Find(Endpoint, query);
        }

        public IList<Agent> CompatibleAgents(long buildId)
        {
            var query = $"{Endpoint}/{buildId}/compatibleAgents";
            var agents = ApiClient.Get<Agents>(query);
            return agents.Agent == null ? new List<Agent>() : agents.Agent.Select(b => b).ToList();
        }
    }
}