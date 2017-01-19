namespace NetCoreTeamCity.Locators
{
    internal class ApiLocator
    {
        public string Value { get; }

        public ApiLocator(string key, string value)
        {
            Value = $"{key}:{value}";
        }
    }
}
