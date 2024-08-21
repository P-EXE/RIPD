using RIPDShared.Models;
using RIPDApi.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

await builder.Services.RegisterControllers();
await builder.Services.RegisterRepos();
await builder.RegisterDatabaseContext();
await builder.Services.RegisterAuthServices();
await builder.Services.RegisterTools();

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