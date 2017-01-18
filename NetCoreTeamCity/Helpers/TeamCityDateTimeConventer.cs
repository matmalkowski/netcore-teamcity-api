using Newtonsoft.Json.Converters;

namespace NetCoreTeamCity.Helpers
{
    internal class TeamCityDateTimeConventer : IsoDateTimeConverter
    {
        public TeamCityDateTimeConventer()
        {
            base.DateTimeFormat = TeamCityDateTimeFormat.DateTimeFormat;
        }
    }
}
