using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDApi.Services;
using System.Data.Common;

namespace RIPDApi.Config;

public static class DatabaseConfig
{
  public static async Task RegisterDatabaseContext(this WebApplicationBuilder builder)
  {
    await ConfigureSQLServerDatabaseContext(builder);
    await ConfigureMongoDBContext(builder);
  }

  public static async Task RegisterTestDatabaseContext(this WebApplicationBuilder builder)
  {
    await ConfigureSQLServerDatabaseContext(builder);
    await ConfigureMongoDBContext(builder);
  }

  private static Task ConfigureSQLServerDatabaseContext(WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<SQLDataBaseContext>(options =>
      options.UseSqlServer(
        builder.Configuration.GetConnectionString("RIPDDB-SQLConnection")
      )
    );

    return Task.CompletedTask;
  }

  private static Task ConfigureSQLiteInMemoryDatabaseContext(WebApplicationBuilder builder)
  {
    builder.Services.AddSingleton<DbConnection>(container =>
    {
      SqliteConnection connection = new("DataSource=sharedInMemoryDB;mode=memory;cache=shared");
      connection.Open();

      return connection;
    });

    builder.Services.AddDbContext<SQLDataBaseContext>((container, options) =>
    {
      var connection = container.GetRequiredService<DbConnection>();
      options.UseSqlite(connection);
    });

    return Task.CompletedTask;
  }

  private static Task ConfigureMongoDBContext(WebApplicationBuilder builder)
  {
    MongoDataBaseSettings mongoDataBaseSettings = builder.Configuration.GetSection("MongoDataBaseSettings").Get<MongoDataBaseSettings>();
    builder.Services.Configure<MongoDataBaseSettings>(builder.Configuration.GetSection("MongoDataBaseSettings"));
    builder.Services.AddDbContext<MongoDataBaseContext>(options =>
      options.UseMongoDB(
        mongoDataBaseSettings.ConnectionString, mongoDataBaseSettings.DatabaseName
      )
    );

    builder.Services.AddSingleton<MongoDBService>();

    return Task.CompletedTask;
  }
}
