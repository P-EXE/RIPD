using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RIPDApp.DataBase;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDApp.ViewModels;
using RIPDApp.Views;

namespace RIPDApp;

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

    #region Services

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddTransient<IHttpService, HttpService>();
    builder.Services.AddHttpClient<IHttpService, HttpService>(options =>
    {
      options.BaseAddress = new("");
    });

    builder.Services.AddTransient<IOwnerService, OwnerService>();
    builder.Services.AddTransient<IFoodService, FoodService>();
    builder.Services.AddTransient<IUserService, UserService>();

    #endregion Services

    #region Pages Views ViewModels

    #region global
    builder.Services.AddSingleton<StatusBarV>();
    builder.Services.AddSingleton<StatusBarVM>();
    #endregion global

    #region food
    builder.Services.AddSingleton<AddFoodPage>();
    builder.Services.AddSingleton<AddFoodVM>();

    builder.Services.AddTransient<FoodListFoodV>();
    builder.Services.AddTransient<FoodListFoodVM>();

    builder.Services.AddTransient<FoodDetailsPage>();
    builder.Services.AddTransient<FoodDetailsVM>();

    builder.Services.AddTransient<NewFoodPage>();
    builder.Services.AddTransient<NewFoodVM>();
    #endregion food

    #region user
    builder.Services.AddSingleton<ProfilePage>();
    builder.Services.AddSingleton<ProfileVM>();
    builder.Services.AddSingleton<OwnerProfilePage>();
    builder.Services.AddSingleton<OwnerProfileVM>();

    builder.Services.AddTransient<RegisterPage>();
    builder.Services.AddTransient<RegisterVM>();

    builder.Services.AddTransient<LoginPage>();
    builder.Services.AddTransient<LoginVM>();
    #endregion user

    builder.Services.AddSingleton<SettingsPage>();
    builder.Services.AddSingleton<SettingsVM>();

    builder.Services.AddTransient<SettingsDevPage>();
    builder.Services.AddTransient<SettingsDevVM>();

    #endregion Pages Views ViewModels

#if DEBUG
    builder.Logging.AddDebug();
#endif

    return builder.Build();
  }
}
