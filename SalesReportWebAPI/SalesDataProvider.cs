using System;
using System.Collections.Generic;
using SalesReportWebAPI.Models;

namespace SalesReportWebAPI
{
  internal static class SalesDataProvider
  {
    public static List<Article> SalesData = new List<Article>
    {
      new Article(Guid.NewGuid().ToString("N"), 12.50, new DateTime(2021, 4, 10, 15, 30, 00), "Pizza"),
      new Article(Guid.NewGuid().ToString("N"), 15.99, new DateTime(2021, 4, 10, 15, 30, 00), "Cake"),
      new Article(Guid.NewGuid().ToString("N"), 5.99, new DateTime(2021, 4, 12, 16, 20, 45), "Bepis"),
      new Article(Guid.NewGuid().ToString("N"), 2.50, new DateTime(2021, 4, 11, 15, 20, 45), "Sparkling water"),
      new Article(Guid.NewGuid().ToString("N"), 2.39, new DateTime(2021, 4, 10, 14, 40, 45), "Water"),
      new Article(Guid.NewGuid().ToString("N"), 199.99, new DateTime(2021, 4, 12, 14, 50, 45), "Silver bracelet"),
      new Article(Guid.NewGuid().ToString("N"), 499.99, new DateTime(2021, 4, 12, 14, 50, 45), "Cooking mixer"),
      new Article(Guid.NewGuid().ToString("N"), 59.99, new DateTime(2021, 4, 11, 15, 15, 45), "Toy robot"),
      new Article(Guid.NewGuid().ToString("N"), 10.00, new DateTime(2021, 4, 11, 15, 10, 45), "Winter hat"),
      new Article(Guid.NewGuid().ToString("N"), 0.49, new DateTime(2021, 4, 10, 16, 20, 45), "Chewing gum")
    };
  }
}
