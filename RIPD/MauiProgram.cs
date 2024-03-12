using Microsoft.Extensions.Logging;
using RIPD.DataServices;
using RIPD.Pages;
using RIPD.ViewModels;
using RIPD.Views;
using Camera.MAUI;
using ZXing.Net.Maui.Controls;

namespace RIPD
{
  public static class MauiProgram
  {
    public static MauiApp CreateMauiApp()
    {
      var builder = MauiApp.CreateBuilder();
      builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })
        .UseMauiCameraView()
        .UseBarcodeReader();

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

      builder.Services.AddTransient<BarcodeScannerPage>();
      builder.Services.AddTransient<BarcodeScannerZXingV>();
      builder.Services.AddTransient<BarcodeScannerZXingVM>();
      #endregion Pages Views ViewModels

#if DEBUG
      builder.Logging.AddDebug();
#endif

      return builder.Build();
    }
  }
}
