using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI.Interfaces
{
  public interface IDataStore
  {
    Task AddSoldArticle(Article article);
    Task<IEnumerable<DailySaleCount>> GetSoldArticle(DateTime startDay, DateTime endDay);
    Task<IEnumerable<DailyRevenue>> GetRevenues(DateTime startDay, DateTime endDay);
    Task<IEnumerable<ArticleRevenue>> GetRevenuesByArticleType();
    Task<IEnumerable<DailyArticleRevenue>> GetRevenuesByArticleType(DateTime startDay, DateTime endDay);
  }
}
