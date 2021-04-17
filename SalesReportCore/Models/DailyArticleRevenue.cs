using System;
using System.Collections.Generic;

namespace SalesReportCore.Models
{
  /// <summary>
  ///   Model class for daily revenue of an article
  /// </summary>
  public class DailyArticleRevenue
  {
    /// <summary>
    ///   Sale day of the revenue
    /// </summary>
    public DateTime SaleDay { get; set; }
    /// <summary>
    ///   Collection of article revenues 
    /// </summary>
    public IEnumerable<ArticleRevenue> Revenues { get; set; }

    /// <summary>
    ///   Creates an instance of <see cref="DailyArticleRevenue"/>
    /// </summary>
    /// <remarks>
    ///   Default ctor is for serialization purposes only, otherwise a parametrized one should be used
    /// </remarks>
    public DailyArticleRevenue()
    {
      //Purposefully created for JSON (de)serialization
    }

    /// <summary>
    ///   Creates an instance of <see cref="DailyArticleRevenue"/>
    /// </summary>
    /// <param name="saleDay"></param>
    /// <param name="revenues"></param>
    public DailyArticleRevenue(DateTime saleDay, IEnumerable<ArticleRevenue> revenues)
    {
      SaleDay = saleDay;
      Revenues = revenues;
    }
  }
}
