using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RIPDApi.Data;
using RIPDApi.Repos;
using RIPDApi.Services;
using RIPDShared.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Data.Common;

namespace RIPDApi;

public static class ServiceRegisterer
{
  public static void RegisterRepos(this WebApplicationBuilder builder)
  {
    builder.Services.AddTransient<IFoodRepo, FoodRepo>();
    builder.Services.AddTransient<IWorkoutRepo, WorkoutRepo>();
    builder.Services.AddTransient<IDiaryRepo, DiaryRepo>();
    builder.Services.AddTransient<IUserRepo, UserRepo>();
  }

  public static void RegisterIdentity(this WebApplicationBuilder builder)
  {
    builder.Services.AddAuthorization();
    builder.Services.AddIdentityApiEndpoints<AppUser>()
      .AddEntityFrameworkStores<SQLDataBaseContext>();
  }

  public static void RegisterAutoMapper(this WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
  }

  public static void RegisterSwagger(this WebApplicationBuilder builder)
  {
    builder.Services.AddSwaggerGen(options =>
    {
      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
      });
      options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
  }

  public static void RegisterSQLServerDBContext(this WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<SQLDataBaseContext>(options =>
      options.UseSqlServer(
        builder.Configuration.GetConnectionString("RIPDDB-SQLConnection")
      )
    );
  }

  public static void RegisterSQLiteDBContext(this WebApplicationBuilder builder)
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
  }

  public static void RegisterMongoDBContext(this WebApplicationBuilder builder)
  {
    MongoDataBaseSettings mongoDataBaseSettings = builder.Configuration.GetSection("MongoDataBaseSettings").Get<MongoDataBaseSettings>();
    builder.Services.Configure<MongoDataBaseSettings>(builder.Configuration.GetSection("MongoDataBaseSettings"));
    builder.Services.AddDbContext<MongoDataBaseContext>(options =>
      options.UseMongoDB(
        mongoDataBaseSettings.ConnectionString, mongoDataBaseSettings.DatabaseName
      )
    );

    builder.Services.AddSingleton<MongoDBService>();
  }
}
