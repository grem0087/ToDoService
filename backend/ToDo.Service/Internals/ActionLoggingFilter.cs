using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Api.Internals
{
    public class ActionLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        public ActionLoggingFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ActionLoggingFilter>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var message = new
            {
                actionName = context.ActionDescriptor.DisplayName,
                parameters = context.ActionArguments
            };

            _logger.LogInformation(JsonConvert.SerializeObject(message));
            await next();
        }
    }
}
