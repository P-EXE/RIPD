using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using epj.RouteGenerator;

namespace RIPDApp;

[AutoRoutes("Page")]
public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
      .UseMauiApp<App>()
      .UseMauiCommunityToolkit()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });

    builder.RegisterEverything();

#if DEBUG
    builder.Logging.AddDebug();
#endif

    return builder.Build();
  }
}
