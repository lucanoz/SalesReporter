using System;
using System.Collections.Generic;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI
{
  public static class SalesDataProvider
  {
    public static List<Article> SalesData = new List<Article>
    {
      new Article(Guid.NewGuid().ToString("N"), 12.50, new DateTime(2021, 4, 10, 15, 30, 00), ArticleType.Food),
      new Article(Guid.NewGuid().ToString("N"), 15.99, new DateTime(2021, 4, 10, 15, 30, 00), ArticleType.Food),
      new Article(Guid.NewGuid().ToString("N"), 5.99, new DateTime(2021, 4, 12, 16, 20, 45), ArticleType.Drinks),
      new Article(Guid.NewGuid().ToString("N"), 2.50, new DateTime(2021, 4, 11, 15, 20, 45), ArticleType.Drinks),
      new Article(Guid.NewGuid().ToString("N"), 2.39, new DateTime(2021, 4, 10, 14, 40, 45), ArticleType.Drinks),
      new Article(Guid.NewGuid().ToString("N"), 199.99, new DateTime(2021, 4, 12, 14, 50, 45), ArticleType.Accessories),
      new Article(Guid.NewGuid().ToString("N"), 499.99, new DateTime(2021, 4, 12, 14, 50, 45), ArticleType.Tools),
      new Article(Guid.NewGuid().ToString("N"), 59.99, new DateTime(2021, 4, 11, 15, 15, 45), ArticleType.Toys),
      new Article(Guid.NewGuid().ToString("N"), 10.00, new DateTime(2021, 4, 11, 15, 10, 45), ArticleType.Accessories),
      new Article(Guid.NewGuid().ToString("N"), 0.49, new DateTime(2021, 4, 10, 16, 20, 45), ArticleType.Food)
    };
  }
}
