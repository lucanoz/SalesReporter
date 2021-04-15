using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI.Interfaces
{
  public interface IDataStore
  {
    Task AddSoldArticle(Article article);
    Task<IEnumerable<DailyArticleSale>> GetSoldArticle(DateTime startDay, DateTime endDay);
    Task<IEnumerable<DailyRevenue>> GetRevenues(DateTime startDay, DateTime endDay);
    Task<IEnumerable<DailyRevenue>> GetRevenuesByArticleType(DateTime startDay, DateTime endDay);
  }
}
