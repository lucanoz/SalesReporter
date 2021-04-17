using System;
using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  /// <summary>
  ///   Model class for total daily revenue
  /// </summary>
  public class DailyRevenue
  {
    /// <summary>
    ///   Day of the revenue
    /// </summary>
    public DateTime Day { get; }
    /// <summary>
    ///   Total revenue in EUR(€)
    /// </summary>
    public double Amount { get; }

    /// <summary>
    ///   Creates an instance of <see cref="DailyRevenue"/>
    /// </summary>
    /// <param name="day"></param>
    /// <param name="amount"></param>
    public DailyRevenue(DateTime day, double amount)
    {
      Day = day;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
