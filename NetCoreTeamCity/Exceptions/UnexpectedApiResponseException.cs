using System;

namespace NetCoreTeamCity.Exceptions
{
    public class UnexpectedApiResponseException : Exception
    {
        public UnexpectedApiResponseException(string message) : base(message)
        {
        }
    }
}
