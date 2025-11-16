using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace LeaveManagement.WebUI.Middleware
{
    public class CentralizedExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CentralizedExceptionHandling> _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public CentralizedExceptionHandling(
            RequestDelegate next,
            ILogger<CentralizedExceptionHandling> logger,
            ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next;
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                var problem = _problemDetailsFactory.CreateProblemDetails(
                    context,
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    title: "An unexpected error occurred.",
                    detail: ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
