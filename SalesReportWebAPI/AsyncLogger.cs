using System;
using System.Text;
using System.Threading;

namespace SalesReportWebAPI
{
  /// <summary>
  ///   Log message level
  /// </summary>
  public enum LogLevel
  {
    /// <summary>
    ///   For logging messages with general information
    /// </summary>
    Info,
    /// <summary>
    ///   For logging messages when something unexpected occurs or attention worthy (but not error causing) occurs
    /// </summary>
    Warning,
    /// <summary>
    ///   For logging messages when an error occurs
    /// </summary>
    Error
  }

  /// <summary>
  ///   Logger for collecting messages in an async context
  /// </summary>
  public static class AsyncLogger
  {
    private const string Indent = "| ";
    private static readonly AsyncLocal<(string, StringBuilder)> RequestId = new AsyncLocal<(string, StringBuilder)>();

    /// <summary>
    ///   Starts a log message for given request id
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="initialMessage"></param>
    public static void StartRequestLog(string requestId, string initialMessage)
    {
      RequestId.Value = (requestId, new StringBuilder($"{DateTime.Now:s} {initialMessage}{Environment.NewLine}"));
    }

    /// <summary>
    ///   Add message to the collected log messages
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void Log(LogLevel level, string message)
    {
      if (RequestId.Value.Item1 == null) return;

      switch (level)
      {
        case LogLevel.Info:
          LogInfo(message);
          break;
        case LogLevel.Warning:
          LogWarning(message);
          break;
        case LogLevel.Error:
          LogError(message);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(level), level, null);
      }
    }

    /// <summary>
    ///   Add message and exception to the collected log messages
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void LogException(string message, Exception exception)
    {
      if (RequestId.Value.Item1 == null) return;
      
      RequestId.Value.Item2.AppendLine($"{Indent} E {message}. Exception: {exception}");
    }

    /// <summary>
    ///   Format and return collected log messages as a single message
    /// </summary>
    /// <param name="endMessage"></param>
    /// <returns></returns>
    public static string EndRequestLog(string endMessage = null)
    {
      if (RequestId.Value.Item1 == null) return null;
      
      if (!string.IsNullOrEmpty(endMessage))
      {
        RequestId.Value.Item2.AppendLine($"{DateTime.Now:s} {endMessage}");
      }

      return RequestId.Value.Item2.ToString();
    }

    private static void LogInfo(string message)
    {
      RequestId.Value.Item2.AppendLine($"{Indent} I {message}");
    }

    private static void LogWarning(string message)
    {
      RequestId.Value.Item2.AppendLine($"{Indent} W {message}");
    }

    private static void LogError(string message)
    {
      RequestId.Value.Item2.AppendLine($"{Indent} E {message}");
    }
  }
}
