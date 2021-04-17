using System;
using System.Collections.Generic;

namespace SalesReportWebAPI.Models
{
  public class DailyArticleRevenue
  {
    public DateTime Day { get; }
    public IEnumerable<ArticleRevenue> Revenues { get; }

    public DailyArticleRevenue(DateTime day, IEnumerable<ArticleRevenue> revenues)
    {
      Day = day;
      Revenues = revenues;
    }
  }
}
