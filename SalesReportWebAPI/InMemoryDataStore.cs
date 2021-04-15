using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SalesReportWebAPI.Interfaces;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI
{
  public class InMemoryDataStore : IDataStore
  {
    private readonly List<Article> _soldArticles;

    public InMemoryDataStore()
    {
      _soldArticles = SalesDataProvider.SalesData;
    }

    public Task AddSoldArticle(Article article)
    {
      _soldArticles.Add(article);
      
      return Task.CompletedTask;
    }

    //public IEnumerable<Article> GetSoldArticle(DateTime targetDay)
    //{
    //  return _soldArticles.Where(article => article.SellTime.Date == targetDay.Date);
    //}

    public Task<IEnumerable<DailyArticleSale>> GetSoldArticle(DateTime startDay, DateTime endDay)
    {
      IEnumerable<DailyArticleSale> result = _soldArticles
        .Where(x => x.SellTime.Date >= startDay.Date && x.SellTime.Date <= endDay.Date)
        .GroupBy(article => article.SellTime.Date)
        .Select(articleGrouping => new DailyArticleSale(articleGrouping.Key, articleGrouping.Count()));

      return Task.FromResult(result);
    }

    //public double GetRevenue(DateTime targetDay)
    //{
    //  return _soldArticles
    //    .Where(article => article.SellTime.Date == targetDay.Date)
    //    .Sum(article => article.Price);
    //}

    public Task<IEnumerable<DailyRevenue>> GetRevenues(DateTime startDay, DateTime endDay)
    {
      IEnumerable<DailyRevenue> result = _soldArticles
        .Where(article => article.SellTime.Date >= startDay.Date && article.SellTime.Date <= endDay.Date)
        .GroupBy(article => article.SellTime.Date)
        .Select(articleGrouping => new DailyRevenue(articleGrouping.Key, articleGrouping.Sum(article => article.Price)));

      return Task.FromResult(result);
    }

    public Task<IEnumerable<DailyRevenue>> GetRevenuesByArticleType(DateTime startDay, DateTime endDay)
    {
      IEnumerable<DailyRevenue> result = _soldArticles
        .Where(article => article.SellTime.Date >= startDay.Date && article.SellTime.Date <= endDay.Date)
        .GroupBy(article => new { article.SellTime.Date, article.Type })
        .Select(articleGrouping => new DailyRevenue(articleGrouping.Key.Date, articleGrouping.Sum(article => article.Price)));
      
      return Task.FromResult(result);
    }
  }
}
