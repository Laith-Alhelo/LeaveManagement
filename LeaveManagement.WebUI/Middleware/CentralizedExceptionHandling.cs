using System.Net;
using LeaveManagement.Infrastructure.Logging;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            ProblemDetailsFactory problemDetailsFactory
           )
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

                var exceptionLogger = context.RequestServices.GetRequiredService<IExceptionLogger>();
                await exceptionLogger.LogAsync(ex, context);

                context.Response.Redirect("/Home/Error");
                
            }
        }
    }
}
