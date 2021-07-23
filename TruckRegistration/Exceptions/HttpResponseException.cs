using System;
using System.Net;

namespace TruckRegistration.Exceptions
{
    /// <summary>
    /// A custom and simple exception handler for the purpose of this application.
    /// </summary>
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Status will be the HTTP Status code of the exception
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Optional value object to pass parameters to the exception handler.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Since this exception handler will only benefit from the HTTP Status code, it is the only contructor implemented.
        /// </summary>
        /// <param name="status">The HTTP Status code of the exception.</param>
        public HttpResponseException(HttpStatusCode status)
        {
            Status = (int)status;
        }
    }
}
