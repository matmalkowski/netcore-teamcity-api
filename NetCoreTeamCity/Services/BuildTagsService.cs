using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NetCoreTeamCity.Api;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
using NetCoreTeamCity.Locators.Build;
using NetCoreTeamCity.Models;

namespace NetCoreTeamCity.Services
{
    internal class BuildTagsService : IBuildTagsService
    {
        private readonly ITeamCityApiClient _apiClient;

        public BuildTagsService(ITeamCityApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IList<string> Get(long buildId)
        {
            try
            {
                var tags = _apiClient.Get<Tags>(GetUri(buildId));
                return tags?.Tag != null ? tags.Tag.Select(t => t.Name).ToList() : new List<string>();
            }
            catch (HttpException exception)
            {
                if (exception.StatusCode == HttpStatusCode.NotFound) return new List<string>();
                throw;
            }
        }

        public IList<string> Replace(long buildId, string tag)
        {
            throw new NotImplementedException();
        }

        public IList<string> Replace(long buildId, IList<string> tags)
        {
            throw new NotImplementedException();
        }

        public IList<string> Add(long buildId, string tag)
        {
            throw new NotImplementedException();
        }

        public IList<string> Add(long buildId, IList<string> tags)
        {
            throw new NotImplementedException();
        }

        private string GetUri(long buildId)
        {
            return $"builds/{buildId}/tags/";
        }
    }
}
