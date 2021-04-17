using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public Task<IEnumerable<DailySaleCount>> GetSoldArticle(DateTime startDay, DateTime endDay)
    {
      Func<Article, bool> dayCondition;

      if (startDay.Day == endDay.Day)
      {
        dayCondition = article => article.SellTime.Date == startDay.Date;
      }
      else
      {
        dayCondition = article => article.SellTime.Date >= startDay.Date && article.SellTime.Date <= endDay.Date;
      }

      IEnumerable<DailySaleCount> result = _soldArticles
        .Where(dayCondition)
        .GroupBy(article => article.SellTime.Date)
        .Select(articleGrouping => new DailySaleCount(
          articleGrouping.Key, 
          articleGrouping.Count())
        );

      return Task.FromResult(result);
    }

    public Task<IEnumerable<DailyRevenue>> GetRevenues(DateTime startDay, DateTime endDay)
    {
      Func<Article, bool> dayCondition;

      if (startDay.Day == endDay.Day)
      {
        dayCondition = article => article.SellTime.Date == startDay.Date;
      }
      else
      {
        dayCondition = article => article.SellTime.Date >= startDay.Date && article.SellTime.Date <= endDay.Date;
      }

      IEnumerable<DailyRevenue> result = _soldArticles
        .Where(dayCondition)
        .GroupBy(article => article.SellTime.Date)
        .Select(articleGrouping => new DailyRevenue(
          articleGrouping.Key, 
          articleGrouping.Sum(article => article.Price))
        );

      return Task.FromResult(result);
    }

    public Task<IEnumerable<ArticleRevenue>> GetRevenuesByArticleType()
    {
      IEnumerable<ArticleRevenue> result = _soldArticles
        .GroupBy(article => article.Name, StringComparer.InvariantCultureIgnoreCase)
        .Select(typeGrouping => new ArticleRevenue(
          typeGrouping.Key,
          typeGrouping.Sum(article => article.Price))
        );

      return Task.FromResult(result);
    }

    public Task<IEnumerable<DailyArticleRevenue>> GetRevenuesByArticleType(DateTime startDay, DateTime endDay)
    {
      Func<Article, bool> dayCondition;

      if (startDay.Day == endDay.Day)
      {
        dayCondition = article => article.SellTime.Date == startDay.Date;
      }
      else
      {
        dayCondition = article => article.SellTime.Date >= startDay.Date && article.SellTime.Date <= endDay.Date;
      }

      IEnumerable<DailyArticleRevenue> result = _soldArticles
        .Where(dayCondition)
        .GroupBy(article => article.SellTime.Date)
        .Select(dailyGrouping => new DailyArticleRevenue(
          dailyGrouping.Key,
          dailyGrouping
              .GroupBy(article => article.Name, StringComparer.InvariantCultureIgnoreCase)
              .Select(typeGrouping => new ArticleRevenue(
                typeGrouping.Key,
                typeGrouping.Sum(article => article.Price)))
        ));

      return Task.FromResult(result);
    }
  }
}
