using System;
using System.Net;

namespace NetCoreTeamCity.Exceptions
{
    public class HttpException : Exception
    {
        private readonly int _httpStatusCode;

        public HttpException(int httpStatusCode)
        {
            this._httpStatusCode = httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode)
        {
            this._httpStatusCode = (int)httpStatusCode;
        }

        public HttpException(int httpStatusCode, string message) : base(message)
        {
            this._httpStatusCode = httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            this._httpStatusCode = (int)httpStatusCode;
        }

        public HttpException(int httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            this._httpStatusCode = httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            this._httpStatusCode = (int)httpStatusCode;
        }

        public int StatusCode { get { return _httpStatusCode; } }
    }
}
