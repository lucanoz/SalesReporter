using System;
using SalesReportCore.Utility;

namespace SalesReportCore.Models
{
  /// <summary>
  ///   Model class for total daily revenue
  /// </summary>
  public class DailyRevenue
  {
    /// <summary>
    ///   Sale day of the revenue
    /// </summary>
    public DateTime SaleDay { get; set; }
    /// <summary>
    ///   Total revenue in EUR(€)
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    ///   Creates an instance of <see cref="DailyRevenue"/>
    /// </summary>
    /// <remarks>
    ///   Default ctor is for serialization purposes only, otherwise a parametrized one should be used
    /// </remarks>
    public DailyRevenue()
    {
      //Purposefully created for JSON (de)serialization
    }

    /// <summary>
    ///   Creates an instance of <see cref="DailyRevenue"/>
    /// </summary>
    /// <param name="saleDay"></param>
    /// <param name="amount"></param>
    public DailyRevenue(DateTime saleDay, double amount)
    {
      SaleDay = saleDay;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
