using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RIPDApi.Data;
using System.Data.Common;

namespace RIPDApi.IntegrationTests;

internal static class DatabaseSetup
{
  public static async Task SetupTestDB(this IServiceCollection services)
  {
    await services.RemoveProductionSQLDB();
    await services.RemoveProductionMongoDB();
    /*services.AddSQLiteTestDB();*/
    await services.AddTestSQLDB();
    await services.AddTestMongoDB();
  }

  private static Task RemoveProductionSQLDB(this IServiceCollection services)
  {
    ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(d =>
      d.ServiceType == typeof(DbContextOptions<SQLDataBaseContext>)
    );
    services.Remove(dbContextDescriptor!);

    ServiceDescriptor? dbConnectionDescriptor = services.SingleOrDefault(d =>
      d.ServiceType == typeof(DbConnection)
    );
    services.Remove(dbConnectionDescriptor!);

    return Task.CompletedTask;
  }

  private static Task RemoveProductionMongoDB(this IServiceCollection services)
  {
    ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(d =>
      d.ServiceType == typeof(DbContextOptions<MongoDataBaseContext>)
    );
    services.Remove(dbContextDescriptor!);

    ServiceDescriptor? dbConnectionDescriptor = services.SingleOrDefault(d =>
      d.ServiceType == typeof(DbConnection)
    );
    services.Remove(dbConnectionDescriptor!);

    return Task.CompletedTask;
  }

  private static Task AddTestSQLDB(this IServiceCollection services)
  {
    services.AddDbContext<SQLDataBaseContext>(options =>
    {
      options.UseSqlServer("Server=.\\SQLEXPRESS;Database=RIPDIntegrationTests;Trusted_Connection=true;TrustServerCertificate=true;");
      options.EnableDetailedErrors();
      options.EnableSensitiveDataLogging();
    });

    SQLDataBaseContext context = services.BuildServiceProvider().GetRequiredService<SQLDataBaseContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    return Task.CompletedTask;
  }

  private static Task AddTestMongoDB(this IServiceCollection services)
  {
    services.AddDbContext<MongoDataBaseContext>(options =>
    {
      options.UseMongoDB("mongodb://localhost:27017/", "RIPDIntegrationTest");
      options.EnableDetailedErrors();
      options.EnableSensitiveDataLogging();
    });

    SQLDataBaseContext context = services.BuildServiceProvider().GetRequiredService<SQLDataBaseContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    return Task.CompletedTask;
  }

  private static Task AddSQLiteTestDB(this IServiceCollection services)
  {
    services.AddSingleton<DbConnection>(container =>
    {
      SqliteConnection connection = new("Data Source=RIPDIntegrationTests;Mode=Memory;Cache=Shared");
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

    SQLDataBaseContext context = services.BuildServiceProvider().GetRequiredService<SQLDataBaseContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    return Task.CompletedTask;
  }
}
