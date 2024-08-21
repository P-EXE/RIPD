using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace RIPDApi.Config;

public static class ToolsConfig
{
  public static async Task RegisterTools(this IServiceCollection services)
  {
    await ConfigureAutoMapper(services);
    await ConfigureSwagger(services);
  }

  private static Task ConfigureAutoMapper(IServiceCollection services)
  {
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    return Task.CompletedTask;
  }

  private static Task ConfigureSwagger(IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
      });
      options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    return Task.CompletedTask;
  }
}
