using RIPDShared.Models;
using RIPDApi.Config;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

await services.RegisterControllers();
await services.RegisterRepos();
await services.RegisterTools();
await services.RegisterAuthServices();

if (builder.Environment.IsDevelopment())
{
  await services.RegisterDebugTools();
  await services.RegisterSQLiteInMemory();
  await services.RegisterMongoServerTestDatabase();
}
else if (builder.Environment.IsStaging())
{
  // Placeholder
}
else if (builder.Environment.IsProduction())
{
  await services.RegisterSQLServerContainer();
  await services.RegisterMongoServerContainer();
}

var app = builder.Build();

app.MapGroup("/api/user").MapIdentityApi<AppUser>();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else if (app.Environment.IsStaging())
{
  // Placeholder
}
else if (app.Environment.IsProduction())
{
  app.UseHttpsRedirection();
}

app.Run();

public partial class Program { }