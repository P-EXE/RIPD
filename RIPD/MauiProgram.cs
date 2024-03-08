using Camera.MAUI;
using Microsoft.Extensions.Logging;
using RIPD.DataServices;
using RIPD.Pages;
using RIPD.ViewModels;
using RIPD.Views;

namespace RIPD
{
  public static class MauiProgram
  {
    public static MauiApp CreateMauiApp()
    {
      var builder = MauiApp.CreateBuilder();
      builder
        .UseMauiApp<App>()
        .UseMauiCameraView()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

      #region Data Services
      builder.Services.AddSingleton<IFoodDataService, FoodDataService>();
      #endregion Data Services

      #region Pages Views ViewModels
      builder.Services.AddSingleton<StatusBarV>();
      builder.Services.AddSingleton<StatusBarVM>();

      builder.Services.AddSingleton<AddFoodPage>();
      builder.Services.AddSingleton<AddFoodVM>();

      builder.Services.AddTransient<FoodListFoodV>();
      builder.Services.AddTransient<FoodListFoodVM>();

      builder.Services.AddTransient<FoodDetailsPage>();
      builder.Services.AddTransient<FoodDetailsVM>();

      builder.Services.AddTransient<NewFoodPage>();
      builder.Services.AddTransient<NewFoodVM>();
      #endregion Pages Views ViewModels

#if DEBUG
      builder.Logging.AddDebug();
#endif

      return builder.Build();
    }
  }
}
