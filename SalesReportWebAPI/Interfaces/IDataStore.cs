using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesReportCore.Models;

namespace SalesReportWebAPI.Interfaces
{
  /// <summary>
  ///   Interface for data storage access
  /// </summary>
  public interface IDataStore
  {
    /// <summary>
    ///   Stores a new instance of sold article
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    Task AddSoldArticle(Article article);

    /// <summary>
    ///   Fetches the amount of articles sold between the specified days (start and end days included)
    /// </summary>
    /// <param name="startDay"></param>
    /// <param name="endDay"></param>
    /// <returns></returns>
    Task<IEnumerable<DailySaleCount>> GetSoldArticle(DateTime startDay, DateTime endDay);

    /// <summary>
    ///   Fetches daily revenues between the specified days (start and end days included)
    /// </summary>
    /// <param name="startDay"></param>
    /// <param name="endDay"></param>
    /// <returns></returns>
    Task<IEnumerable<DailyRevenue>> GetRevenues(DateTime startDay, DateTime endDay);

    /// <summary>
    ///   Fetches total revenue based on article
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ArticleRevenue>> GetRevenuesByArticle();

    /// <summary>
    ///   Fetches daily revenue between the specified days (start and end days included) based on article
    /// </summary>
    /// <param name="startDay"></param>
    /// <param name="endDay"></param>
    /// <returns></returns>
    Task<IEnumerable<DailyArticleRevenue>> GetRevenuesByArticle(DateTime startDay, DateTime endDay);
  }
}
