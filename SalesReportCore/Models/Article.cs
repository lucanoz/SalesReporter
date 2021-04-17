using System;
using SalesReportCore.Utility;

namespace SalesReportCore.Models
{
  /// <summary>
  ///   Model class for an article instance
  /// </summary>
  public class Article
  {
    /// <summary>32</summary>
    public const int MaxArticleUniqueNumberLength = 32;

    /// <summary>
    ///   Uniquely identifying number of an article (up to <inheritdoc cref="MaxArticleUniqueNumberLength"/>  characters)
    /// </summary>
    public string UniqueNumber { get; set; }

    /// <summary>
    ///   Article price in EUR(€)
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    ///   Time of the article's sale
    /// </summary>
    public DateTime SellTime { get; set; }
    /// <summary>
    ///   Name of the article
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///   Creates an instance of <see cref="Article"/>
    /// </summary>
    /// <remarks>
    ///   Default ctor is for serialization purposes only, otherwise a parametrized one should be used
    /// </remarks>
    public Article()
    {
      //Purposefully created for JSON (de)serialization
    }

    /// <summary>
    ///   Creates an instance of <see cref="Article"/>
    /// </summary>
    /// <param name="uniqueNumber"></param>
    /// <param name="price"></param>
    /// <param name="sellTime"></param>
    /// <param name="name"></param>
    public Article(string uniqueNumber, double price, DateTime sellTime, string name)
    {
      UniqueNumber = Enforce.Length(uniqueNumber, MaxArticleUniqueNumberLength);
      Price = Enforce.NotNegative(price);
      SellTime = sellTime;
      Name = Enforce.NotNullOrEmpty(name);
    }

    public override string ToString()
    {
      return $"{nameof(UniqueNumber)}: {UniqueNumber}; {nameof(Price)}: {Price}; {nameof(SellTime)}: {SellTime:s}; {nameof(Name)}: {Name};";
    }
  }
}
