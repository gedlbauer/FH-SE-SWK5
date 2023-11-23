using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CurrencyConverter.Api.Filters
{
  public class LoggingFilter : IActionFilter
  {
    private readonly ILogger logger;

    public LoggingFilter(ILogger<LoggingFilter> logger) => this.logger = logger;

    public void OnActionExecuting(ActionExecutingContext context)
    {
      logger.LogInformation($"Starting action {context.ActionDescriptor.DisplayName} ");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
      logger.LogInformation($"Finished action {context.ActionDescriptor.DisplayName} ");
    }

  }
}
