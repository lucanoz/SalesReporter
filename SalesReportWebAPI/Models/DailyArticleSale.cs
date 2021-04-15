using System;

namespace SalesReportWebAPI.Models
{
  public class DailyArticleSale
  {
    public DateTime Day { get; }
    public int Amount { get; }

    public DailyArticleSale(DateTime day, int amount)
    {
      Day = day;
      Amount = amount;
    }
  }
}
