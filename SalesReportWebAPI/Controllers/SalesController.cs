using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesReportCore.Models;
using SalesReportWebAPI.Interfaces;

namespace SalesReportWebAPI.Controllers
{
  /// <summary>
  ///   Controller for sales related endpoints
  /// </summary>
  [Produces("application/json")]
  [ApiController]
  [Route("api/[controller]")]
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
    public async Task<ActionResult<IAsyncEnumerable<DailyRevenue>>> GetRevenue()
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetRevenue)}");
      IEnumerable<DailyRevenue> result = await _dataStore.GetRevenues(DateTime.MinValue, DateTime.Today);

      if (!result.Any())
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetRevenue)} returned no values");
        return NoContent();
      }
      
      return Ok(result);
    }

    /// <summary>
    ///   Fetches revenue for a specified day
    /// </summary>
    /// <param name="targetDay"></param>
    /// <returns></returns>
    [HttpGet("revenue/{targetDay:datetime}")]
    public async Task<ActionResult<DailyRevenue>> GetRevenue(DateTime targetDay)
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetRevenue)}. Input: {targetDay:s}");
      DailyRevenue result = (await _dataStore.GetRevenues(targetDay.Date, targetDay.Date)).SingleOrDefault();

      if (result == null)
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetRevenue)} returned no values");
        return NoContent();
      }

      return Ok(result);
    }

    /// <summary>
    ///   Fetches revenue for every article summed over all days
    /// </summary>
    /// <returns></returns>
    [HttpGet("revenueByArticle/total")]
    public async Task<ActionResult<IEnumerable<ArticleRevenue>>> GetTotalRevenueByArticle()
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetTotalRevenueByArticle)}");
      IEnumerable<ArticleRevenue> result = await _dataStore.GetRevenuesByArticle();

      if (!result.Any())
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetTotalRevenueByArticle)} returned no values");
        return NoContent();
      }

      return Ok(result);
    }

    /// <summary>
    ///   Fetches daily revenue for every article
    /// </summary>
    /// <returns></returns>
    [HttpGet("revenueByArticle")]
    public async Task<ActionResult<IEnumerable<DailyArticleRevenue>>> GetRevenueByArticle()
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetRevenueByArticle)}");
      IEnumerable<DailyArticleRevenue> result = await _dataStore.GetRevenuesByArticle(DateTime.MinValue, DateTime.Today);

      if (!result.Any())
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetRevenueByArticle)} returned no values");
        return NoContent();
      }

      return Ok(result);
    }

    /// <summary>
    ///   Fetches revenue for every article for a specified day
    /// </summary>
    /// <param name="targetDay"></param>
    /// <returns></returns>
    [HttpGet("revenueByArticle/{targetDay:datetime}")]
    public async Task<ActionResult<DailyArticleRevenue>> GetRevenueByArticle(DateTime targetDay)
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetRevenueByArticle)}. Input: {targetDay:s}");
      DailyArticleRevenue result = (await _dataStore.GetRevenuesByArticle(targetDay.Date, targetDay.Date)).SingleOrDefault();
      
      if (result == null)
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetRevenueByArticle)} returned no values");
        return NoContent();
      }

      return Ok(result);
    }

    /// <summary>
    ///   Fetches the amount of articles sold until today
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DailySaleCount>>> GetSales()
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetSales)}");
      IEnumerable<DailySaleCount> result = await _dataStore.GetSoldArticle(DateTime.MinValue, DateTime.Today);
      
      if (!result.Any())
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetSales)} returned no values");
        return NoContent();
      }

      return Ok(result);
    }

    /// <summary>
    ///   Fetches the amount of articles sold for a specified day
    /// </summary>
    /// <param name="targetDay"></param>
    /// <returns></returns>
    [HttpGet("{targetDay:datetime}")]
    public async Task<ActionResult<DailySaleCount>> GetSales(DateTime targetDay)
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(GetSales)}. Input: {targetDay:s}");
      DailySaleCount result = (await _dataStore.GetSoldArticle(targetDay.Date, targetDay.Date)).SingleOrDefault();

      if (result == null)
      {
        AsyncLogger.Log(LogLevel.Warning, $"{nameof(GetSales)} returned no values");
        return NoContent();
      }

      return Ok(result);
    }

    /// <summary>
    ///   Submits an instance of a new sold article
    /// </summary>
    /// <param name="article"></param>
    [HttpPost]
    public async Task<ActionResult> AddArticleSale([FromBody] Article article)
    {
      AsyncLogger.Log(LogLevel.Info, $"Operation: {nameof(AddArticleSale)}. Input: {article}");
      await _dataStore.AddSoldArticle(article);

      return Ok();
    }
  }
}
