using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Yak.Core.Common;

namespace Yak.Core.ErrorHandling;

using Microsoft.AspNetCore.Diagnostics;

public class ApplicationErrorHandler(ILogger<ApplicationErrorHandler> Logger) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
    CancellationToken cancellationToken)
  {
    if (exception is ApplicationError error)
    {
      Logger.LogTrace($"Exception of type: {exception.GetType()} has been catched. Attempting to handle...");
      Logger.LogInformation($"Exception of type: {exception.GetType()} has been catched. Attempting to handle...");

      var response = new ApplicationErrorResponse(
        error.Code,
        error.Message
      );

      httpContext.Response.StatusCode = response.StatusCode;;
      await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

      return true;
    }

    return false;
  }
}
