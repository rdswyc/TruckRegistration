using System;
using System.Net;

namespace TruckRegistration.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }

        public object Value { get; set; }

        public HttpResponseException(HttpStatusCode status)
        {
            Status = (int)status;
        }
    }
}
