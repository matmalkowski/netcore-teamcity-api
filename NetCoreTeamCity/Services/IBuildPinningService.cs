namespace NetCoreTeamCity.Services
{
    public interface IBuildPinningService
    {
        void Pin(long buildId, string comment = null);
        void UnPin(long buildId, string comment = null);
        bool IsPinned(long buildId);
    }
}