using ZXing.Net.Maui.Controls;

namespace RIPDApp.Config;

public static class ToolsConfig
{
  public static Task RegisterTools(this MauiAppBuilder builder)
  {
    RegisterAutoMapper(builder);
    RegisterUsings(builder);

    return Task.CompletedTask;
  }

  private static Task RegisterAutoMapper(MauiAppBuilder builder)
  {
    builder.Services.AddAutoMapper(options =>
    {
      options.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
      // Warning: could cause errors, disable when no longer needed.
      options.AllowNullDestinationValues = true;
    });

    return Task.CompletedTask;
  }

  private static Task RegisterUsings(MauiAppBuilder builder)
  {
    builder.UseBarcodeReader();

    return Task.CompletedTask;
  }
}
