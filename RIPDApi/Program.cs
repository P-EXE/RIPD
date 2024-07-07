using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using RIPDApi.Data;
using RIPDShared.Models;
using RIPDApi.Services;
using RIPDApi.Repos;
using System.Text.Json.Serialization;
using System.Data.Common;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
  });

builder.Services.AddEndpointsApiExplorer();

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

#region SQL Server

builder.Services.AddDbContext<SQLDataBaseContext>(options =>
  options.UseSqlServer(
    builder.Configuration.GetConnectionString("RIPDDB-SQLConnection")
  )
);

#endregion SQL Server

#region SQLite in Memory

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

#endregion SQLite in Memory



#region MongoDB

MongoDataBaseSettings mongoDataBaseSettings = builder.Configuration.GetSection("MongoDataBaseSettings").Get<MongoDataBaseSettings>();
builder.Services.Configure<MongoDataBaseSettings>(builder.Configuration.GetSection("MongoDataBaseSettings"));
builder.Services.AddDbContext<MongoDataBaseContext>(options =>
  options.UseMongoDB(
    mongoDataBaseSettings.ConnectionString, mongoDataBaseSettings.DatabaseName
  )
);

builder.Services.AddSingleton<MongoDBService>();

#endregion MongoDB

#region Repos

builder.Services.AddTransient<IFoodRepo, FoodRepo>();
builder.Services.AddTransient<IWorkoutRepo, WorkoutRepo>();
builder.Services.AddTransient<IDiaryRepo, DiaryRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();

#endregion Repos

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
  .AddEntityFrameworkStores<SQLDataBaseContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapGroup("/api/user").MapIdentityApi<AppUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }