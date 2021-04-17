using System;
using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  public class DailyRevenue
  {
    public DateTime Day { get; }
    public double Amount { get; }

    public DailyRevenue(DateTime day, double amount)
    {
      Day = day;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
