using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Text.Json;

namespace RIPDApi.IntegrationTests.Setup;

public class DefaultFixture : WebApplicationFactory<Program>, IDisposable
{
  public readonly HttpClient Client;
  public readonly JsonSerializerOptions JsonOpt;

  public DefaultFixture()
  {
    Client = CreateClient();

    JsonOpt = new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);

    builder.ConfigureTestServices(services =>
    {
      services.SetupTestDB();
    });
    builder.UseEnvironment("Development");
  }

  public override ValueTask DisposeAsync()
  {
    return base.DisposeAsync();
  }
}
