using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SalesReportWebAPI.Middleware
{
#pragma warning disable CS1591
  public class LoggingMiddleware
  {
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      var sw = Stopwatch.StartNew();

      try
      {
        AsyncLogger.StartRequestLog(context.TraceIdentifier, $"=> Starting request ${context.TraceIdentifier}");
        await _next.Invoke(context);
      }
      catch (Exception e)
      {
        AsyncLogger.LogException(e.Message, e);
      }
      finally
      {
        sw.Stop();
        Debug.WriteLine(AsyncLogger.EndRequestLog($"<= Ending request ${context.TraceIdentifier}. Total duration: {sw.ElapsedMilliseconds} ms"));
      }
    }
  }
#pragma warning restore CS1591
}
