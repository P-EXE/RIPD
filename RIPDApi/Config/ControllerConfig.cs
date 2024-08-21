using System.Text.Json.Serialization;

namespace RIPDApi.Config;

public static class ControllerConfig
{
  public static async Task RegisterControllers(this IServiceCollection services)
  {
    IMvcBuilder builder = services.AddControllers();
    await ConfigureJsonOptions(builder);
  }

  private static Task ConfigureJsonOptions(IMvcBuilder builder)
  {
    builder.AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      options.JsonSerializerOptions.WriteIndented = true;
    });
    return Task.CompletedTask;
  }
}
