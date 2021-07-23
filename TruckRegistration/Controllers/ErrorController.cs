using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TruckRegistration.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = (System.Web.Http.HttpResponseException)context.Error;

            return Problem(
                statusCode: (int)error.Response.StatusCode
            );
        }
    }
}
