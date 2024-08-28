namespace RIPDApp.Config;

public static class ToolsConfig
{
  public static Task RegisterTools(this IServiceCollection services)
  {
    services.AddAutoMapper(options =>
    {
      options.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
      // Warning: could cause errors, disable when no longer needed.
      options.AllowNullDestinationValues = true;
    });

    return Task.CompletedTask;
  }
}
