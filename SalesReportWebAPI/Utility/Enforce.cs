using System;

namespace SalesReportWebAPI.Utility
{
  public static class Enforce
  {
    public static string NotNullOrEmpty(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException("String must have a value");
      }

      return value;
    }

    public static string Length(string value, int maxLength)
    {
      NotNullOrEmpty(value);

      if (value.Length > maxLength)
      {
        throw new ArgumentException($"Value exceeded max specified length `{maxLength}`");
      }

      return value;
    }

    public static double NotNegative(double value)
    {
      if (value < 0)
      {
        throw new ArgumentException("Value must be greater or equal to zero");
      }

      return value;
    }

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
