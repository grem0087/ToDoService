using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ToDo.Api.Internals
{
    using System.Linq;

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var ss = httpContext.Request.Headers.ToArray();
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await SendExceptionMessage(httpContext);
            }
        }

        private async Task SendExceptionMessage(HttpContext httpContext)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = @"application/json";

            var responseDto = new { Code = 500, Message = "Unknown error." };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseDto));
        }

    }
}
