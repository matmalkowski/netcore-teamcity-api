using System.Collections.Generic;

namespace NetCoreTeamCity.Locators.Build
{
    public class BuildField
    {
        private readonly List<string> _fields;

        internal BuildField()
        {
            _fields = new List<string>();
        }

        public BuildField Default()
        {
            return BuildTypeId().Href().Id().Number().State().Status().WebUrl();
        }

        public BuildField BuildTypeId()
        {
            _fields.Add("buildTypeId");
            return this;
        }

        public BuildField Href()
        {
            _fields.Add("href");
            return this;
        }

        public BuildField Id()
        {
            _fields.Add("id");
            return this;
        }

        public BuildField Number()
        {
            _fields.Add("number");
            return this;
        }

        public BuildField State()
        {
            _fields.Add("state");
            return this;
        }

        public BuildField Status()
        {
            _fields.Add("status");
            return this;
        }

        public BuildField WebUrl()
        {
            _fields.Add("webUrl");
            return this;
        }

        public BuildField BranchName()
        {
            _fields.Add("branchName");
            return this;
        }

        public BuildField QueuedDate()
        {
            _fields.Add("queuedDate");
            return this;
        }

        public BuildField StartDate()
        {
            _fields.Add("startDate");
            return this;
        }

        public BuildField FinishDate()
        {
            _fields.Add("finishDate");
            return this;
        }

        public BuildField StatusText()
        {
            _fields.Add("statusText");
            return this;
        }

        public BuildField Revisions()
        {
            _fields.Add("revisions");
            return this;
        }

        public BuildField BuildType()
        {
            _fields.Add("buildType");
            return this;
        }

        public BuildField Triggered()
        {
            _fields.Add("triggered");
            return this;
        }

        public BuildField LastChanges()
        {
            _fields.Add("lastChanges(change)");
            return this;
        }

        public BuildField Agent()
        {
            _fields.Add("agent");
            return this;
        }

        public BuildField Properties()
        {
            _fields.Add("properties(property)");
            return this;
        }

        public BuildField TestOccurrences()
        {
            _fields.Add("testOccurrences");
            return this;
        }

        public BuildField PersonalFlag()
        {
            _fields.Add("personal");
            return this;
        }

        public BuildField Comment()
        {
            _fields.Add("comment");
            return this;
        }

        public BuildField RunningInfo()
        {
            _fields.Add("running-info");
            return this;
        }

        internal string GetFieldsQueryString()
        {
            if (_fields.Count == 0) this.Default();
            return $"build({string.Join(",", _fields)})";
        }
    }
}
