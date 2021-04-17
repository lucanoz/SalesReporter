using System;
using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  public class DailySaleCount
  {
    public DateTime Day { get; }
    public int Amount { get; }

    public DailySaleCount(DateTime day, int amount)
    {
      Day = day;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
