using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TruckRegistration.Exceptions
{
    /// <summary>
    /// Action filter implementation to register the custom HttpResponseException and be included in the container services configuration.
    /// </summary>
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// Set the order to maxValue - 1000 so that other optional filters can be prioritized.
        /// </summary>
        public int Order { get; } = int.MaxValue - 1000;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
