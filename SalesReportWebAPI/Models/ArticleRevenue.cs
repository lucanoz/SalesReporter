using SalesReportWebAPI.Utility;

namespace SalesReportWebAPI.Models
{
  public class ArticleRevenue
  {
    public string Name { get; }
    public double Amount { get; }

    public ArticleRevenue(string name, double amount)
    {
      Name = name;
      Amount = Enforce.NotNegative(amount);
    }
  }
}
