using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesReportCore.Models;
using SalesReportWebAPIClient;
using Xunit;

namespace SalesReportWebAPI.Test.Integration
{
  public class SalesControllerTests  : IClassFixture<TestFixture<Startup>>
  {
    private SalesReportClient Client { get; }

    public SalesControllerTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task GetRevenueForAllDays_Success()
    {
      List<DailyRevenue> result = (await Client.GetRevenue()).ToList();

      Assert.Equal(
        SalesDataProvider.SalesData.GroupBy(x => x.SellTime.Day).Count(), 
        result.Count);

      foreach (DailyRevenue dailyRevenue in result)
      {
        Assert.Equal(
          SalesDataProvider.SalesData
            .Where(x => x.SellTime.Day == dailyRevenue.SaleDay.Day)
            .Sum(x => x.Price),
          dailyRevenue.Amount);
      }
    }

    [Theory]
    [InlineData("2021", "4", "10")]
    [InlineData("2021", "4", "11")]
    [InlineData("2021", "4", "12")]
    public async Task GetRevenueForDay_Success(string year, string month, string day)
    {
      var targetDay = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
      var result = await Client.GetRevenue(targetDay);

      Assert.Equal(
        SalesDataProvider.SalesData
          .Where(x => x.SellTime.Day == targetDay.Day)
          .Sum(x => x.Price),
        result.Amount);
    }

    [Theory]
    [InlineData("1990", "4", "10")]
    [InlineData("2021", "5", "10")]
    [InlineData("2021", "4", "01")]
    public async Task GetRevenueForDay_Error(string year, string month, string day)
    {
      var targetDay = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
      DailyRevenue result = await Client.GetRevenue(targetDay);

      Assert.Null(result);
    }

    [Fact]
    public async Task GetTotalRevenueByArticle_Success()
    {
      List<ArticleRevenue> result = (await Client.GetTotalRevenueByArticle()).ToList();

      foreach (ArticleRevenue articleRevenue in result)
      {
        Assert.Equal(
          SalesDataProvider.SalesData
            .Where(x => x.Name.Equals(articleRevenue.Name, StringComparison.InvariantCultureIgnoreCase))
            .Sum(x => x.Price),
          articleRevenue.Amount);
      }
    }

    [Fact]
    public async Task GetDailyRevenueByArticle_Success()
    {
      List<DailyArticleRevenue> result = (await Client.GetRevenueByArticle()).ToList();

      foreach (DailyArticleRevenue dailyArticleRevenue in result)
      {
        List<ArticleRevenue> salesForCurrentDay = SalesDataProvider.SalesData
          .Where(x => x.SellTime.Day == dailyArticleRevenue.SaleDay.Day)
          .GroupBy(x => x.Name)
          .Select(x => new ArticleRevenue(x.Key, x.Sum(y => y.Price)))
          .ToList();
        List<ArticleRevenue> resultSalesForCurrentDay = dailyArticleRevenue.Revenues.ToList();
        
        Assert.Equal(salesForCurrentDay.Count, resultSalesForCurrentDay.Count);

        salesForCurrentDay.Sort((x1, x2) => string.Compare(x1.Name, x2.Name, StringComparison.Ordinal));
        resultSalesForCurrentDay.Sort((x1, x2) => string.Compare(x1.Name, x2.Name, StringComparison.Ordinal));

        for (var i = 0; i < salesForCurrentDay.Count; i++)
        {
          Assert.Equal(salesForCurrentDay[i].Amount, resultSalesForCurrentDay[i].Amount);
        }
      }
    }

    [Theory]
    [InlineData("2021", "4", "10")]
    [InlineData("2021", "4", "11")]
    [InlineData("2021", "4", "12")]
    public async Task GetDailyRevenueByArticleForDay_Success(string year, string month, string day)
    {
      var targetDay = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
      DailyArticleRevenue result = (await Client.GetRevenueByArticle(targetDay));

      List<ArticleRevenue> salesForCurrentDay = SalesDataProvider.SalesData
        .Where(x => x.SellTime.Day == result.SaleDay.Day)
        .GroupBy(x => x.Name)
        .Select(x => new ArticleRevenue(x.Key, x.Sum(y => y.Price)))
        .ToList();
      List<ArticleRevenue> resultSalesForCurrentDay = result.Revenues.ToList();

      Assert.Equal(salesForCurrentDay.Count, resultSalesForCurrentDay.Count);

      salesForCurrentDay.Sort((x1, x2) => string.Compare(x1.Name, x2.Name, StringComparison.Ordinal));
      resultSalesForCurrentDay.Sort((x1, x2) => string.Compare(x1.Name, x2.Name, StringComparison.Ordinal));

      for (var i = 0; i < salesForCurrentDay.Count; i++)
      {
        Assert.Equal(salesForCurrentDay[i].Amount, resultSalesForCurrentDay[i].Amount);
      }
    }

    [Theory]
    [InlineData("1990", "4", "10")]
    [InlineData("2021", "5", "10")]
    [InlineData("2021", "4", "01")]
    public async Task GetDailyRevenueByArticleForDay_Error(string year, string month, string day)
    {
      var targetDay = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
      DailyArticleRevenue result = await Client.GetRevenueByArticle(targetDay);

      Assert.Null(result);
    }

    [Fact]
    public async Task GetDailySalesCount_Success()
    {
      IEnumerable<DailySaleCount> result = await Client.GetSales();

      foreach (DailySaleCount dailySaleCount in result)
      {
        Assert.Equal(
          SalesDataProvider.SalesData
            .Count(x => x.SellTime.Day == dailySaleCount.SaleDay.Day), 
          dailySaleCount.Amount);
      }
    }

    [Theory]
    [InlineData("2021", "4", "10")]
    [InlineData("2021", "4", "11")]
    [InlineData("2021", "4", "12")]
    public async Task GetSalesCountForDay_Success(string year, string month, string day)
    {
      var targetDay = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
      DailySaleCount result = await Client.GetSales(targetDay);

      Assert.Equal(
        SalesDataProvider.SalesData
          .Count(x => x.SellTime.Day == result.SaleDay.Day),
        result.Amount);
    }

    [Theory]
    [InlineData("1990", "4", "10")]
    [InlineData("2021", "5", "10")]
    [InlineData("2021", "4", "01")]
    public async Task GetSalesCountForDay_Error(string year, string month, string day)
    {
      var targetDay = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
      DailySaleCount result = await Client.GetSales(targetDay);

      Assert.Null(result);
    }

    [Fact]
    public async Task AddNewSale_Success()
    {
      var article = new Article(Guid.NewGuid().ToString("N"), 123.45, new DateTime(2000, 1, 1), "Shoes");
      await Client.AddArticleSale(article);

      Assert.Contains(article.UniqueNumber, SalesDataProvider.SalesData.Select(x => x.UniqueNumber));
    }

    [Fact]
    public async Task AddNewSaleAndGettingRevenue_Success()
    {
      var day1 = new DateTime(2021, 4, 10, 16, 30, 00);
      var day2 = new DateTime(2021, 4, 11, 16, 30, 00);
      var article = new Article(Guid.NewGuid().ToString("N"), 12.50, day1, "Pizza");
      await Client.AddArticleSale(article);
      article.SellTime = new DateTime(2021, 4, 10, 17, 30, 00);
      await Client.AddArticleSale(article);
      article.SellTime = day2;
      await Client.AddArticleSale(article);
      article.SellTime = new DateTime(2021, 4, 11, 17, 30, 00);
      await Client.AddArticleSale(article);

      ArticleRevenue totalRevenueForPizza = (await Client.GetTotalRevenueByArticle()).Single(x => x.Name.Equals("Pizza"));
      Assert.Equal(12.50 * 5, totalRevenueForPizza.Amount);

      DailyArticleRevenue revenueForPizzaDay1 = await Client.GetRevenueByArticle(day1);
      DailyArticleRevenue revenueForPizzaDay2 = await Client.GetRevenueByArticle(day2);
      Assert.Equal(12.50 * 3, revenueForPizzaDay1.Revenues.Single(x => x.Name.Equals("Pizza")).Amount);
      Assert.Equal(12.50 * 2, revenueForPizzaDay2.Revenues.Single(x => x.Name.Equals("Pizza")).Amount);
    }
  }
}
