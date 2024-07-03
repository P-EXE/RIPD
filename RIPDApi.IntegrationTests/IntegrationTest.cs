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
  protected readonly HttpClient TestHttpClient;
  protected readonly JsonSerializerOptions JsonOptions;
  private readonly AppUser_Create _createUser;
  protected static AppUser TestUser;
  private static BearerToken _bearerToken;

  public IntegrationTest()
  {

    TestHttpClient = CreateClient();

    JsonOptions = new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    _createUser = new()
    {
      Email = "test1User@mail.com",
      Password = "P455w0rd!",
    };

    RegisterTestUser();
    LoginTestUser();
    GetTestUser();
    ReAuthorizeHttpClient();
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);

    builder.ConfigureTestServices(services =>
    {
      var dbContextDescriptor = services.SingleOrDefault(d =>
        d.ServiceType == typeof(DbContextOptions<SQLDataBaseContext>)
      );

      services.Remove(dbContextDescriptor);

      var dbConnectionDescriptor = services.SingleOrDefault(d =>
        d.ServiceType == typeof(DbConnection)
      );

      services.Remove(dbConnectionDescriptor);

      // Create open SqliteConnection so EF won't automatically close it.
      services.AddSingleton<DbConnection>(container =>
      {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
      });

      services.AddDbContext<SQLDataBaseContext>((container, options) =>
      {
        var connection = container.GetRequiredService<DbConnection>();
        options.UseSqlite(connection);
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
      });

      /*services.AddSqlite<SQLDataBaseContext>("DataSource=:memory:");*/
    });
    builder.UseEnvironment("Development");
  }

  private void RegisterTestUser()
  {
    HttpResponseMessage response = TestHttpClient.PostAsJsonAsync("api/user/register", _createUser, JsonOptions).Result;
    response.EnsureSuccessStatusCode();
  }

  private void LoginTestUser()
  {
    HttpResponseMessage response = TestHttpClient.PostAsJsonAsync("api/user/login", _createUser).Result;
    response.EnsureSuccessStatusCode();
    BearerToken? bt = JsonSerializer.Deserialize<BearerToken>(response.Content.ReadAsStringAsync().Result, JsonOptions);
    if (bt != null)
    {
      _bearerToken = bt;
    }
    else
    {
      throw new ArgumentNullException(nameof(bt));
    }
    TestHttpClient.DefaultRequestHeaders.Authorization = new(bt.TokenType, bt.AccessToken);
  }

  private void GetTestUser()
  {
    HttpResponseMessage response = TestHttpClient.GetAsync("api/user/self/private").Result;
    response.EnsureSuccessStatusCode();
    AppUser? user = JsonSerializer.Deserialize<AppUser>(response.Content.ReadAsStringAsync().Result, JsonOptions);
    if (user != null)
    {
      TestUser = user;
    }
    else
    {
      throw new ArgumentNullException(nameof(user));
    }
  }

  public async void ReAuthorizeHttpClient()
  {
    HttpResponseMessage response = await TestHttpClient.PostAsJsonAsync("api/user/refresh", new Dictionary<string, string>()
    {
      ["refreshToken"] = _bearerToken.RefreshToken,
    });
    response.EnsureSuccessStatusCode();
    BearerToken? bt = JsonSerializer.Deserialize<BearerToken>(await response.Content.ReadAsStringAsync(), JsonOptions);
    _bearerToken = bt;
    TestHttpClient.DefaultRequestHeaders.Authorization = new(bt.TokenType, bt.AccessToken);
  }
}