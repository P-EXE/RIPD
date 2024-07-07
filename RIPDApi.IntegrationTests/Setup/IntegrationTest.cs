using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RIPDApi.Data;
using RIPDShared.Models;
using System.Data.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

public class IntegrationTest : WebApplicationFactory<Program>
{
  private protected readonly HttpClient TestHttpClient;
  private protected readonly JsonSerializerOptions JsonOptions;
  private protected static AppUser TestUser = IdentitySetup.GenerateTestUser().Result;

  public IntegrationTest()
  {
    TestHttpClient = CreateClient();
    TestHttpClient.DefaultRequestHeaders.Authorization = new("TestSheme", "Test");
    JsonOptions = new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);

    builder.ConfigureTestServices(services =>
    {
      // DBContext
      ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(d =>
        d.ServiceType == typeof(DbContextOptions<SQLDataBaseContext>)
      );
      services.Remove(dbContextDescriptor);

      services.AddSingleton<DbConnection>(container =>
      {
        SqliteConnection connection = new("DataSource=:memory:");
        connection.Open();

        return connection;
      });

      services.AddDbContext<SQLDataBaseContext>((container, options) =>
      {
        DbConnection connection = container.GetRequiredService<DbConnection>();
        options.UseSqlite(connection);
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
      });

      SQLDataBaseContext sQLDataBaseContext = services.BuildServiceProvider()
        .GetRequiredService<SQLDataBaseContext>();

      sQLDataBaseContext.Database.EnsureDeleted();
      sQLDataBaseContext.Database.EnsureCreated();
      sQLDataBaseContext.Users.Add(TestUser);

      // Auth
      services.AddAuthentication("TestScheme")
        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("TestScheme", options => { });
    });

    builder.UseEnvironment("Development");
  }
}