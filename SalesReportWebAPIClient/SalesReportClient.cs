using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SalesReportCore.Models;

namespace SalesReportWebAPIClient
{
  public class SalesReportClient : IDisposable
  {
    private const string DefaultBaseAddress = "http://localhost:56891/";
    private const string ApiBase = "api/sales";

    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };

    public SalesReportClient(string baseAddress)
    {
      if (string.IsNullOrEmpty(baseAddress))
      {
        baseAddress = DefaultBaseAddress;
      }
      else
      {
        if (!baseAddress.EndsWith('/'))
        {
          baseAddress += '/';
        }
      }

      _client = new HttpClient
      {
        BaseAddress = new Uri(baseAddress)
      };
    }

    public SalesReportClient(HttpClient httpClient)
    {
      _client = httpClient;
    }

    public Task<IEnumerable<DailyRevenue>> GetRevenue()
    {
      return GetAsync<IEnumerable<DailyRevenue>>($"{ApiBase}/revenue");
    }

    public Task<DailyRevenue> GetRevenue(DateTime targetDay)
    {
      return GetAsync<DailyRevenue>($"{ApiBase}/revenue/{targetDay:s}");
    }

    public Task<IEnumerable<ArticleRevenue>> GetTotalRevenueByArticle()
    {
      return GetAsync<IEnumerable<ArticleRevenue>>($"{ApiBase}/revenueByArticle/total");
    }

    public Task<IEnumerable<DailyArticleRevenue>> GetRevenueByArticle()
    {
      return GetAsync<IEnumerable<DailyArticleRevenue>>($"{ApiBase}/revenueByArticle");
    }

    public Task<DailyArticleRevenue> GetRevenueByArticle(DateTime targetDay)
    {
      return GetAsync<DailyArticleRevenue>($"{ApiBase}/revenueByArticle/{targetDay:s}");
    }

    public Task<IEnumerable<DailySaleCount>> GetSales()
    {
      return GetAsync<IEnumerable<DailySaleCount>>($"{ApiBase}");
    }

    public Task<DailySaleCount> GetSales(DateTime targetDay)
    {
      return GetAsync<DailySaleCount>($"{ApiBase}/{targetDay:s}");
    }

    public Task AddArticleSale(Article article)
    {
      return PostAsync($"{ApiBase}", article);
    }

    public void Dispose()
    {
      _client?.Dispose();
    }

    private async Task<T> GetAsync<T>(string requestUri)
    {
      HttpResponseMessage response = await _client.GetAsync(requestUri);
      response.EnsureSuccessStatusCode();

      if (response.StatusCode != HttpStatusCode.NoContent)
      {
        var result = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), _options);
        return result;
      }

      return default;
    }

    private async Task PostAsync<T>(string requestUri, T input)
    {
      HttpContent content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
      HttpResponseMessage response = await _client.PostAsync(requestUri, content);
      response.EnsureSuccessStatusCode();
    }
  }
}
