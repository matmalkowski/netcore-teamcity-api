using System.Linq;

namespace NetCoreTeamCity.Models
{
    internal static class BuildModelToBuildConventer
    {
        public static Build Convert(this BuildModel model)
        {
            var build = new Build
            {
                Id = model.Id,
                Number = model.Number,
                Status = model.Status,
                State = model.State,
                BranchName = model.BranchName,
                BuildTypeId = model.BuildTypeId,
                Href = model.Href,
                WebUrl = model.WebUrl,
                StatusText = model.StatusText,
                StartDate = model.StartDate,
                FinishDate = model.FinishDate,
                QueuedDate = model.QueuedDate,
                BuildType = model.BuildType,
                Triggered = model.Triggered,
                Agent = model.Agent,
                TestOccurrences = model.TestOccurrences,
                Comment = model.Comment,
                Personal = model.Personal,
                RunningInfo = model.RunningInfo
            };

            if (model.LastChanges?.Change != null)
            {
                build.LastChanges = model.LastChanges.Change.Select(c => c.Convert()).ToList();
            }

            if (model.Revisions?.Revision != null)
                build.Revisions = model.Revisions.Revision;

            if (model.Properties?.Property != null)
                build.Properties = model.Properties.Property;

            return build;
        }
    }
}
