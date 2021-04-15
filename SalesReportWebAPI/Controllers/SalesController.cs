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

    [HttpGet("dailyRevenue")]
    public async Task<IEnumerable<DailyRevenue>> GetDailyRevenue()
    {
      return await _dataStore.GetRevenues(DateTime.MinValue, DateTime.Today);
    }

    [HttpGet("dailyRevenue/{targetDay:datetime}")]
    public async Task<DailyRevenue> GetRevenue(DateTime targetDay)
    {
      return (await _dataStore.GetRevenues(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    [HttpGet("dailySales")]
    public async Task<IEnumerable<DailyArticleSale>> GetDailySales()
    {
      return await _dataStore.GetSoldArticle(DateTime.MinValue, DateTime.Today);
    }

    [HttpGet("dailySales/{targetDay:datetime}")]
    public async Task<DailyArticleSale> GetDailySales(DateTime targetDay)
    {
      return (await _dataStore.GetSoldArticle(targetDay.Date, targetDay.Date)).SingleOrDefault();
    }

    [HttpPost]
    public async Task AddArticleSale([FromBody] Article article)
    {
      //TODO: serialization
      await _dataStore.AddSoldArticle(article);
    }
  }
}
