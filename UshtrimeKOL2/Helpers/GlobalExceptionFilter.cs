using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.ApplicationInsights;

namespace SOA2024.MovieReview.API.Helpers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        protected readonly TelemetryClient _telemetry;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, TelemetryClient telemetry)
        {
            _logger = logger;
            _telemetry = telemetry;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");
            _telemetry.TrackException(context.Exception);

            var response = new
            {
                Message = "An error occurred while processing your request.",
                Details = context.Exception.Message  
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

}
