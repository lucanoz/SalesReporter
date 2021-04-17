using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SalesReportWebAPIClient;

namespace SalesReportWebAPI.Test.Integration
{
  public class TestFixture<TStartup> : IDisposable where TStartup : class
  {
    private readonly TestServer _testServer;
    public SalesReportClient Client { get; }

    public TestFixture()
    {
      var webHostBuilder = new WebHostBuilder().UseStartup<TStartup>();
      _testServer = new TestServer(webHostBuilder);

      HttpClient httpClient = _testServer.CreateClient();
      httpClient.BaseAddress = new Uri("http://localhost:56891");
      Client = new SalesReportClient(httpClient);
    }

    public void Dispose()
    {
      Client?.Dispose();
      _testServer?.Dispose();
    }
  }
}
