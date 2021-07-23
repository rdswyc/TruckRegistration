using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TruckRegistration.Exceptions;

namespace TruckRegistration.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
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
