using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesReportWebAPI.Interfaces;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI.Controllers
{
  /// <summary>
  ///   Controller for sales related endpoints
  /// </summary>
  [Produces("application/json")]
  [ApiController]
  [Route("[controller]")]
  public class SalesController : ControllerBase
  {
    private readonly IDataStore _dataStore;

    /// <summary>
    ///   Creates a new instance of <see cref="SalesController"/>
    /// </summary>
    /// <param name="dataStore"></param>
    public SalesController(IDataStore dataStore)
    {
      _dataStore = dataStore;
    }

    /// <summary>
    ///   Fetches revenues for every day until today
    /// </summary>
    /// <returns></returns>
    [HttpGet("revenue")]
    public async Task<IEnumerable<DailyRevenue>> GetRevenue()
    {
      return await _dataStore.GetRevenues(DateTime.MinValue, DateTime.Today);
    }

    /// <summary>
    ///   Fetches revenue for a specified day
    /// </summary>
    /// <param name="targetDay"></param>
    /// <returns></returns>
    [HttpGet("revenue/{targetDay:datetime}")]
    public async Task<DailyRevenue> GetRevenue(DateTime targetDay)
    {
      return (await _dataStore.GetRevenues(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    /// <summary>
    ///   Fetches revenue for every article summed over all days
    /// </summary>
    /// <returns></returns>
    [HttpGet("revenueByArticle/total")]
    public async Task<IEnumerable<ArticleRevenue>> GetTotalRevenueByArticle()
    {
      return await _dataStore.GetRevenuesByArticle();
    }

    /// <summary>
    ///   Fetches daily revenue for every article
    /// </summary>
    /// <returns></returns>
    [HttpGet("revenueByArticle")]
    public async Task<IEnumerable<DailyArticleRevenue>> GetRevenueByArticle()
    {
      return await _dataStore.GetRevenuesByArticle(DateTime.MinValue, DateTime.Today);
    }

    /// <summary>
    ///   Fetches revenue for every article for a specified day
    /// </summary>
    /// <param name="targetDay"></param>
    /// <returns></returns>
    [HttpGet("revenueByArticle/{targetDay:datetime}")]
    public async Task<DailyArticleRevenue> GetRevenueByArticle(DateTime targetDay)
    {
      return (await _dataStore.GetRevenuesByArticle(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    /// <summary>
    ///   Fetches the amount of articles sold until today
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<DailySaleCount>> GetSales()
    {
      return await _dataStore.GetSoldArticle(DateTime.MinValue, DateTime.Today);
    }

    /// <summary>
    ///   Fetches the amount of articles sold for a specified day
    /// </summary>
    /// <param name="targetDay"></param>
    /// <returns></returns>
    [HttpGet("{targetDay:datetime}")]
    public async Task<DailySaleCount> GetSales(DateTime targetDay)
    {
      return (await _dataStore.GetSoldArticle(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    /// <summary>
    ///   Submits an instance of a new sold article
    /// </summary>
    /// <param name="article"></param>
    [HttpPost]
    public async Task AddArticleSale([FromBody] Article article)
    {
      await _dataStore.AddSoldArticle(article);
    }
  }
}
