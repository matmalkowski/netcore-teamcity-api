namespace NetCoreTeamCity.Models
{
    internal static class ChangeModelToChangeConventer
    {
        public static Change Convert(this ChangeModel model)
        {
            var change = new Change
            {
                Id = model.Id,
                Version = model.Version,
                Username = model.Username,
                Date = model.Date,
                Href = model.Href,
                WebUrl = model.WebUrl,
                Comment = model.Comment,
                User = model.User,
                VcsRootInstance = model.VcsRootInstance
            };

            if (model.Files?.File != null)
            {
                change.Files = model.Files.File;
            }

            return change;
        }
    }
}
