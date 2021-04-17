using SalesReportCore.Utility;

namespace SalesReportCore.Models
{
  /// <summary>
  ///   Model class for revenue of an article
  /// </summary>
  public class ArticleRevenue
  {
    /// <summary>
    ///   Name of the article
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///   Total revenue in EUR(€)
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    ///   Creates an instance of <see cref="ArticleRevenue"/>
    /// </summary>
    /// <remarks>
    ///   Default ctor is for serialization purposes only, otherwise a parametrized one should be used
    /// </remarks>
    public ArticleRevenue()
    {
      //Purposefully created for JSON (de)serialization
    }

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
