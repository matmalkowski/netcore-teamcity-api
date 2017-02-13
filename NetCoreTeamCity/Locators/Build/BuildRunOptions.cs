using System;
using System.Collections.Generic;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Locators.Build
{
    public class BuildRunOptions
    {
        private readonly BuildModel _model;

        internal BuildRunOptions()
        {
            _model = new BuildModel();
            _model.Properties = new Properties {Property = new List<Property>()};
        }

        public BuildRunOptions BuildType(string buildTypeId)
        {
            _model.BuildTypeId = buildTypeId;
            return this;
        }

        public BuildRunOptions Branch(string branchName)
        {
            _model.BranchName = branchName;
            return this;
        }

        public BuildRunOptions Comment(string comment)
        {
            _model.Comment = new BuildComment { Text = comment };
            return this;
        }

        public BuildRunOptions AsPersonal()
        {
            _model.Personal = true;
            return this;
        }

        public BuildRunOptions OnAgent(int agentId)
        {
            _model.Agent = new Agent
            {
                Id = agentId
            };
            return this;
        }

        public BuildRunOptions OnAgentPool(int poolId)
        {
            throw new NotImplementedException();
        }

        public BuildRunOptions WithCustomProperty(string propertyName, string propertyValue)
        {
            _model.Properties.Property.Add(new Property {Name = propertyName, Value = propertyValue});
            return this;
        }

        public BuildRunOptions OnSpecificChange(int changeId)
        {
            throw new NotImplementedException();
        }

        public BuildRunOptions ForceRebuildAllDependencies()
        {
            throw new NotImplementedException();
        }

        public BuildRunOptions MoveToTopOfQueue()
        {
            throw new NotImplementedException();
        }

        internal BuildModel GetBuildModel()
        {
            return _model;
        }
    }
}