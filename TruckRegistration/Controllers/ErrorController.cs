using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TruckRegistration.Exceptions;

namespace TruckRegistration.Controllers
{
    /// <summary>
    /// Simple error handling controller mapping the route of the exception handling filter.
    /// No unit testing required for the simplicity.
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// This unique route will simply output the HTTP Status code of the error, for simplicity.
        /// No error message or details will be shown.
        /// </summary>
        /// <returns></returns>
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = (HttpResponseException)context.Error;

            return Problem(
                statusCode: error.Status
            );
        }
    }
}
