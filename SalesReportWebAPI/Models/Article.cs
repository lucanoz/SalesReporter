using System;

namespace SalesReportWebAPI.Models
{
  public enum ArticleType
  {
    Food,
    Drinks,
    Accessories,
    Tools,
    Toys
  }

  public class Article
  {
    public string UniqueNumber { get; }
    public double Price { get; }
    public DateTime SellTime { get; }
    public ArticleType Type { get; }

    public Article(string uniqueNumber, double price, DateTime sellTime, ArticleType type)
    {
      //TODO: validate input (unique number to 32 digits, price > 0)
      UniqueNumber = uniqueNumber;
      Price = price;
      SellTime = sellTime;
      Type = type;
    }
  }
}
