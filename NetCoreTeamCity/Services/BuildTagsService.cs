using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetCoreTeamCity.Clients;
using NetCoreTeamCity.Exceptions;
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
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            return Replace(buildId, new List<string>() {tag});
        }

        public IList<string> Replace(long buildId, IList<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));

            var toReplace = GetTagsObjectFromStringList(tags);
            var replaced = _apiClient.Put(GetUri(buildId), toReplace);

            return replaced?.Tag != null ? replaced.Tag.Select(t => t.Name).ToList() : new List<string>();
        }

        public IList<string> Add(long buildId, string tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            return Add(buildId, new List<string>() { tag });
        }

        public IList<string> Add(long buildId, IList<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));

            var toAdd = GetTagsObjectFromStringList(tags);
            var added = _apiClient.Post(GetUri(buildId), toAdd);

            return added?.Tag != null ? added.Tag.Select(t => t.Name).ToList() : new List<string>();
        }

        private Tags GetTagsObjectFromStringList(IList<string> inList)
        {
            var tags = new Tags() { Tag = new List<Tag>() };
            foreach (var tag in inList)
            {
                tags.Tag.Add(new Tag() { Name = tag });
            }
            return tags;
        }

        private string GetUri(long buildId)
        {
            return $"builds/{buildId}/tags/";
        }
    }
}
