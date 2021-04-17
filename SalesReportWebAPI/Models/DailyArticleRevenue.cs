using System;
using System.Collections.Generic;

namespace SalesReportWebAPI.Models
{
  /// <summary>
  ///   Model class for daily revenue of an article
  /// </summary>
  public class DailyArticleRevenue
  {
    /// <summary>
    ///   Day of the revenue
    /// </summary>
    public DateTime Day { get; }
    /// <summary>
    ///   Collection of article revenues 
    /// </summary>
    public IEnumerable<ArticleRevenue> Revenues { get; }

    /// <summary>
    ///   Creates an instance of <see cref="DailyArticleRevenue"/>
    /// </summary>
    /// <param name="day"></param>
    /// <param name="revenues"></param>
    public DailyArticleRevenue(DateTime day, IEnumerable<ArticleRevenue> revenues)
    {
      Day = day;
      Revenues = revenues;
    }
  }
}
