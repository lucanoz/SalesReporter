using System;
using SalesReportCore.Utility;

namespace SalesReportCore.Models
{
  /// <summary>
  ///   Model class for daily sale count of an article
  /// </summary>
  public class DailySaleCount
  {
    /// <summary>
    ///   Sale day of the sale
    /// </summary>
    public DateTime SaleDay { get; set; }
    /// <summary>
    ///   Amount of sales for that saleDay
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    ///   Creates an instance of <see cref="DailySaleCount"/>
    /// </summary>
    /// <remarks>
    ///   Default ctor is for serialization purposes only, otherwise a parametrized one should be used
    /// </remarks>
    public DailySaleCount()
    {
      //Purposefully created for JSON (de)serialization
    }

    /// <summary>
    ///   Creates an instance of <see cref="DailySaleCount"/>
    /// </summary>
    /// <param name="saleDay"></param>
    /// <param name="amount"></param>
    public DailySaleCount(DateTime saleDay, int amount)
    {
      SaleDay = saleDay;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
