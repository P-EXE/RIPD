using Microsoft.Extensions.DependencyInjection;
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
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

      #region Data Services

      builder.Services.AddDbContext<LocalDBContext>();
      LocalDBContext ldbc = new();
      ldbc.Database.EnsureCreated();
      ldbc.Dispose();

      builder.Services.AddSingleton<APIStatusChecker>();
      
      builder.Services.AddSingleton<IUserDataService, UserDataServiceSwitch>();
      builder.Services.AddSingleton<UserDataServiceLocal>();
      builder.Services.AddSingleton<UserDataServiceAPI>();
      builder.Services.AddSingleton<IUserDataService, UserDataServiceSwitch>();
      builder.Services.AddSingleton<IFoodDataService, FoodDataServiceAPI>();

      #endregion Data Services

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
}
