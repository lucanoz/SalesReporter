using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  /// <summary>
  ///   Model class for revenue of an article
  /// </summary>
  public class ArticleRevenue
  {
    /// <summary>
    ///   Name of the article
    /// </summary>
    public string Name { get; }
    /// <summary>
    ///   Total revenue in EUR(€)
    /// </summary>
    public double Amount { get; }

    /// <summary>
    ///   Creates an instance of <see cref="ArticleRevenue"/>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    public ArticleRevenue(string name, double amount)
    {
      Name = name;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
