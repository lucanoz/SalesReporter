using System;
using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  /// <summary>
  ///   Model class for daily sale count of an article
  /// </summary>
  public class DailySaleCount
  {
    /// <summary>
    ///   Day of the sale
    /// </summary>
    public DateTime Day { get; }
    /// <summary>
    ///   Amount of sales for that day
    /// </summary>
    public int Amount { get; }

    /// <summary>
    ///   Creates an instance of <see cref="DailySaleCount"/>
    /// </summary>
    /// <param name="day"></param>
    /// <param name="amount"></param>
    public DailySaleCount(DateTime day, int amount)
    {
      Day = day;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
