using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesReportWebAPI.Interfaces;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class SalesController : ControllerBase
  {
    private readonly IDataStore _dataStore;

    public SalesController(IDataStore dataStore)
    {
      _dataStore = dataStore;
    }

    [HttpGet("revenue")]
    public async Task<IEnumerable<DailyRevenue>> GetRevenue()
    {
      return await _dataStore.GetRevenues(DateTime.MinValue, DateTime.Today);
    }

    [HttpGet("revenue/{targetDay:datetime}")]
    public async Task<DailyRevenue> GetRevenue(DateTime targetDay)
    {
      return (await _dataStore.GetRevenues(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    [HttpGet("revenueByArticle/total")]
    public async Task<IEnumerable<ArticleRevenue>> GetTotalRevenueByArticle()
    {
      return await _dataStore.GetRevenuesByArticleType();
    }

    [HttpGet("revenueByArticle")]
    public async Task<IEnumerable<DailyArticleRevenue>> GetRevenueByArticle()
    {
      return await _dataStore.GetRevenuesByArticleType(DateTime.MinValue, DateTime.Today);
    }

    [HttpGet("revenueByArticle/{targetDay:datetime}")]
    public async Task<DailyArticleRevenue> GetRevenueByArticle(DateTime targetDay)
    {
      return (await _dataStore.GetRevenuesByArticleType(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    [HttpGet]
    public async Task<IEnumerable<DailySaleCount>> GetSales()
    {
      return await _dataStore.GetSoldArticle(DateTime.MinValue, DateTime.Today);
    }

    [HttpGet("{targetDay:datetime}")]
    public async Task<DailySaleCount> GetSales(DateTime targetDay)
    {
      return (await _dataStore.GetSoldArticle(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    [HttpPost]
    public async Task AddArticleSale([FromBody] Article article)
    {
      await _dataStore.AddSoldArticle(article);
    }
  }
}
