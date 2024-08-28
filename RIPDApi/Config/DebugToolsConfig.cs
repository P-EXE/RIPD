namespace RIPDApi.Config;

public static class DebugToolsConfig
{
  public static Task RegisterDebugTools(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    return Task.CompletedTask;
  }
}
