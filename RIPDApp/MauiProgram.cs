using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using epj.RouteGenerator;
using RIPDApp.Config;
using ZXing.Net.Maui.Controls;
using Microcharts.Maui;

namespace RIPDApp;

[AutoRoutes("Page")]
public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
      .UseMauiApp<App>()
      .UseMicrocharts()
      .UseMauiCommunityToolkit()
      .UseBarcodeReader()
      .UseMicrocharts()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });
    var services = builder.Services;

    services.RegisterPages();
    services.RegisterViews();
    services.RegisterViewModels();
    services.RegisterServices();
    services.RegisterTools();
    services.RegisterSQLiteDatabase();
    RoutesConfig.RegisterRoutes();

#if DEBUG
    builder.Logging.AddDebug();
#endif

    return builder.Build();
  }
}
