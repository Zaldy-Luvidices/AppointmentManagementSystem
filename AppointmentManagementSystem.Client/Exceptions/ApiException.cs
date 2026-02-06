using System;
using System.Net;

namespace AppointmentManagementSystem.Client.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode? StatusCode { get; }

        public ApiException(string message, Exception inner = null)
        : base(message, inner) { }

        public ApiException(HttpStatusCode statusCode, string message)
        : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
