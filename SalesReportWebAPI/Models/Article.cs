using System;
using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  public class Article
  {
    public const int MaxArticleUniqueNumberLength = 32;

    public string UniqueNumber { get; set; }
    public double Price { get; set; }
    public DateTime SellTime { get; set; }
    public string Name { get; set; }

    public Article()
    {
    }

    public Article(string uniqueNumber, double price, DateTime sellTime, string name)
    {
      UniqueNumber = Enforce.Length(uniqueNumber, MaxArticleUniqueNumberLength);
      Price = Enforce.NotNegative(price);
      SellTime = sellTime;
      Name = Enforce.NotNullOrEmpty(name);
    }
  }
}
