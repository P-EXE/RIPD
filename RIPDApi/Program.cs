using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using RIPDShared.Models;
using System.Text.Json.Serialization;
using RIPDApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
  });

builder.Services.AddEndpointsApiExplorer();

builder.RegisterRepos();
builder.RegisterSQLServerDBContext();
builder.RegisterMongoDBContext();
builder.RegisterIdentity();
builder.RegisterAutoMapper();
builder.RegisterSwagger();

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