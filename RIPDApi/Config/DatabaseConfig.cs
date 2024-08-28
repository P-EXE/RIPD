using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDApi.Services;
using System.Data.Common;

namespace RIPDApi.Config;

public static class DatabaseConfig
{
  public static async Task RegisterSQLiteInMemory(this IServiceCollection services)
  {
    services.AddDbContext<SQLDataBaseContext>(options =>
      options.UseSqlite("DataSource=sharedInMemoryDB;mode=memory;cache=shared")
    );

    var context = services.BuildServiceProvider().GetRequiredService<SQLDataBaseContext>();
    await context.Database.EnsureCreatedAsync();
  }

  public static async Task RegisterSQLServerTestDatabase(this IServiceCollection services)
  {
    string connectionString = "Server=.\\SQLEXPRESS;Database=RIPDTest;Trusted_Connection=true;TrustServerCertificate=true;";
    services.AddDbContext<SQLDataBaseContext>(options =>
      options.UseSqlServer(connectionString)
    );

    var context = services.BuildServiceProvider().GetRequiredService<SQLDataBaseContext>();
    await context.Database.EnsureCreatedAsync();
  }

  public static async Task RegisterSQLServerContainer(this IServiceCollection services)
  {
    services.AddDbContext<SQLDataBaseContext>(options =>
      options.UseSqlServer(DockerConfig.SQLServerConnectionString)
    );

    var context = services.BuildServiceProvider().GetRequiredService<SQLDataBaseContext>();
    await context.Database.EnsureCreatedAsync();
  }

  public static async Task RegisterMongoServerContainer(this IServiceCollection services)
  {
    services.AddDbContext<MongoDataBaseContext>(options =>
      options.UseMongoDB(DockerConfig.MongoServerConnectionString, DockerConfig.MongoServerDatabase)
    );

    var context = services.BuildServiceProvider().GetRequiredService<MongoDataBaseContext>();
    await context.Database.EnsureCreatedAsync();
  }

  public static async Task RegisterMongoServerTestDatabase(this IServiceCollection services)
  {
    services.AddDbContext<MongoDataBaseContext>(options =>
      options.UseMongoDB("mongodb://localhost:27017/", "RIPDTest")
    );

    var context = services.BuildServiceProvider().GetRequiredService<MongoDataBaseContext>();
    await context.Database.EnsureCreatedAsync();
  }
}
