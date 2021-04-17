using System;

namespace SalesReportWebAPI.Utility
{
  /// <summary>
  ///   Utility class for validation checks
  /// </summary>
  public static class Enforce
  {
    /// <summary>
    ///   Enforces a string to have non-empty value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NotNullOrEmpty(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException("String must have a value");
      }

      return value;
    }

    /// <summary>
    ///   Enforces a string to be within a specified length
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Length(string value, int maxLength)
    {
      NotNullOrEmpty(value);

      if (value.Length > maxLength)
      {
        throw new ArgumentException($"Value exceeded max specified length `{maxLength}`");
      }

      return value;
    }

    /// <summary>
    ///   Enforces a number to be 0 or greater
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static double NotNegative(double value)
    {
      if (value < 0)
      {
        throw new ArgumentException("Value must be greater or equal to zero");
      }

      return value;
    }

    /// <summary>
    ///   Enforces a number to be 0 or greater
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int NotNegative(int value)
    {
      if (value < 0)
      {
        throw new ArgumentException("Value must be greater or equal to zero");
      }

      return value;
    }
  }
}
