using CommunityToolkit.Maui;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RIPDApp.DataBase;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDApp.ViewModels;
using RIPDApp.Views;
using System.Net.Http.Headers;

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

    #region SQLite

    SqliteConnection sqliteConnection = new SqliteConnection(
      builder.Configuration.GetConnectionString("SQLiteConnection")
    );
    sqliteConnection.Open();
    builder.Services.AddDbContext<LocalDBContext>(options =>
      options.UseSqlite(sqliteConnection)
    );

    #endregion SQLite

    #region Services

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddTransient<IHttpService, HttpService>();
    builder.Services.AddHttpClient<IHttpService, HttpService>(options =>
    {
      options.BaseAddress = new(Statics.API.RouteBaseHttp);
      options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.API.BearerToken?.AccessToken ?? "");
    });

    builder.Services.AddTransient<IOwnerService, OwnerService>();
    builder.Services.AddTransient<IFoodService, FoodService>();
    builder.Services.AddTransient<IUserService, UserService>();

    #endregion Services

    #region Pages Views ViewModels

    #region Register & Login

    builder.Services.AddTransient<RegisterPage>();
    builder.Services.AddTransient<RegisterVM>();

    builder.Services.AddTransient<LoginPage>();
    builder.Services.AddTransient<LoginVM>();

    #endregion Register & Login

    #region global

    builder.Services.AddTransient<StatusBarV>();
    builder.Services.AddTransient<StatusBarVM>();

    #endregion global

    #region Home

    builder.Services.AddTransient<HomePage>();
    builder.Services.AddTransient<HomeVM>();

    builder.Services.AddTransient<DiaryPage>();
    builder.Services.AddTransient<DiaryVM>();

    #endregion Home

    #region Feed

    builder.Services.AddTransient<FeedPage>();
    builder.Services.AddTransient<FeedVM>();

    #endregion Feed

    #region food

    builder.Services.AddTransient<AddFoodPage>();
    builder.Services.AddTransient<AddFoodVM>();

    builder.Services.AddTransient<FoodListFoodV>();
    builder.Services.AddTransient<FoodListFoodVM>();

    builder.Services.AddTransient<FoodDetailsPage>();
    builder.Services.AddTransient<FoodDetailsVM>();

    builder.Services.AddTransient<NewFoodPage>();
    builder.Services.AddTransient<NewFoodVM>();

    #endregion food

    #region user

    builder.Services.AddTransient<ProfilePage>();
    builder.Services.AddTransient<ProfileVM>();
    builder.Services.AddTransient<OwnerProfilePage>();
    builder.Services.AddTransient<OwnerProfileVM>();

    builder.Services.AddTransient<UserSearchPage>();
    builder.Services.AddTransient<UserSearchVM>();

    #endregion user

    #region Settings

    builder.Services.AddTransient<SettingsPage>();
    builder.Services.AddTransient<SettingsVM>();

    builder.Services.AddTransient<SettingsDevPage>();
    builder.Services.AddTransient<SettingsDevVM>();

    #endregion Settings

    #endregion Pages Views ViewModels

#if DEBUG
    builder.Logging.AddDebug();
#endif

    return builder.Build();
  }
}
