using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using RIPDApi.Data;
using RIPDShared.Models;
using RIPDApi.Services;
using Microsoft.Data.Sqlite;
using RIPDApi.Repos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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

#region SQLite in Memory

SqliteConnection sqliteConnection = new SqliteConnection(
  builder.Configuration.GetConnectionString("SQLiteConnection")
);
sqliteConnection.Open();
builder.Services.AddDbContext<SQLDataBaseContext>(options =>
  options.UseSqlite(sqliteConnection)
);

builder.Services.AddTransient<Seeding>();

#endregion SQLite in Memory

#region SQL Server

/*builder.Services.AddDbContext<SQLDataBaseContext>(options =>
  options.UseSqlServer(
    builder.Configuration.GetConnectionString("RIPDDB2-SQLConnection")
  )
);*/

#endregion SQL Server

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
