using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using RIPDApp.DataBase;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDApp.ViewModels;
using RIPDApp.Views;
using System.Net.Http.Headers;
using ZXing.Net.Maui.Controls;

namespace RIPDApp;

public static class ServiceRegisterer
{
  public static void RegisterEverything(this MauiAppBuilder builder)
  {
    builder.RegisterServices();
    builder.RegisterPages();
    builder.RegisterViews();
    builder.RegisterViewModels();
    builder.RegisterSQLiteDBContext();
    builder.RegisterAutoMapper();
    builder.RegisterUsings();
    RegisterAllRoutes();
  }

  private static void RegisterServices(this MauiAppBuilder builder)
  {
    builder.Services.AddTransient<IHttpService, HttpService>();
    builder.Services.AddHttpClient<IHttpService, HttpService>(options =>
    {
      options.BaseAddress = new(Statics.API.RouteBaseHttp);
      options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Auth.BearerToken?.AccessToken ?? "");
    });

    builder.Services.AddTransient<IOwnerService, OwnerService>();
    builder.Services.AddTransient<IFoodService, FoodService>();
    builder.Services.AddTransient<IWorkoutService, WorkoutService>();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IDiaryService, DiaryService>();
  }
  private static void RegisterPages(this MauiAppBuilder builder)
  {
    builder.Services.AddScoped<AutoLoginPage>();
    builder.Services.AddTransient<RegisterPage>();
    builder.Services.AddTransient<LoginPage>();

    builder.Services.AddTransient<HomePage>();

    builder.Services.AddTransient<DiaryTodayPage>();
    builder.Services.AddTransient<DiaryWeekPage>();
    builder.Services.AddTransient<DiaryMonthPage>();

    builder.Services.AddTransient<DiaryEntryFoodCreatePage>();
    builder.Services.AddTransient<DiaryEntryWorkoutCreatePage>();

    builder.Services.AddTransient<FoodSearchPage>();
    builder.Services.AddTransient<WorkoutSearchPage>();
    builder.Services.AddTransient<UserSearchPage>();

    builder.Services.AddTransient<FoodDetailsPage>();
    builder.Services.AddTransient<FoodCreatePage>();
    builder.Services.AddTransient<FoodUpdatePage>();
    builder.Services.AddTransient<FoodViewPage>();

    builder.Services.AddTransient<WorkoutCreatePage>();

    builder.Services.AddTransient<BodyMetricPage>();
    builder.Services.AddTransient<BodyMetricCreatePage>();
    builder.Services.AddTransient<BodyMetricViewPage>();

    builder.Services.AddTransient<BarcodeScannerPage>();

    builder.Services.AddTransient<UserProfilePage>();
    builder.Services.AddScoped<UserProfileUpdatePage>();

    builder.Services.AddTransient<SettingsPage>();
    builder.Services.AddTransient<SettingsDevPage>();
  }
  private static void RegisterViews(this MauiAppBuilder builder)
  {
    builder.Services.AddTransient<StatusBarV>();

    builder.Services.AddTransient<FoodListFoodV>();
  }
  private static void RegisterViewModels(this MauiAppBuilder builder)
  {
    builder.Services.AddTransient<StatusBarVM>();

    builder.Services.AddTransient<RegisterLoginVM>();

    builder.Services.AddTransient<HomeVM>();

    builder.Services.AddTransient<DiaryVM>();

    builder.Services.AddTransient<DiaryEntryVM>();

    builder.Services.AddTransient<FoodSearchVM>();
    builder.Services.AddTransient<WorkoutSearchVM>();
    builder.Services.AddTransient<UserSearchVM>();

    builder.Services.AddTransient<FoodListFoodVM>();

    builder.Services.AddTransient<FoodDetailsVM>();
    builder.Services.AddTransient<WorkoutDetailsVM>();

    builder.Services.AddTransient<BodyMetricVM>();
    builder.Services.AddTransient<BodyMetricDetailsVM>();

    builder.Services.AddTransient<ScannerVM>();

    builder.Services.AddTransient<UserProfileVM>();

    builder.Services.AddTransient<SettingsVM>();
    builder.Services.AddTransient<SettingsDevVM>();
  }
  private static void RegisterSQLiteDBContext(this MauiAppBuilder builder)
  {
    builder.Services.AddDbContext<LocalDBContext>(options =>
      options.UseSqlite(Statics.LocalDB.SQLiteConnection)
    );
  }
  private static void RegisterAutoMapper(this MauiAppBuilder builder)
  {
    builder.Services.AddAutoMapper(options =>
    {
      options.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
      // Warning: could cause errors, disable when no longer needed.
      options.AllowNullDestinationValues = true;
    });
  }
  private static void RegisterUsings(this MauiAppBuilder builder)
  {
    builder.UseBarcodeReader();
  }
  private static void RegisterJsonSerializerOptions(this MauiAppBuilder builder)
  {

  }

  /// <summary>
  /// All routes which are registered here are registered in their respective methods according to their start point or origin
  /// </summary>
  public static void RegisterAllRoutes()
  {
    RegisterAuthRoutes();
    RegisterHomeRoutes();
    RegisterDiaryRoutes();
    RegisterFoodSearchRoutes();
    RegisterWorkoutSearchRoutes();
    RegisterBodyMetricRoutes();
    RegisterFoodDetailsRoutes();
    RegisterProfileRoutes();
    RegisterSettingsRoutes();
  }
  private static void RegisterAuthRoutes()
  {
    Routing.RegisterRoute(nameof(AutoLoginPage), typeof(AutoLoginPage));
    Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
  }
  private static void RegisterHomeRoutes()
  {
    Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
  }
  private static void RegisterDiaryRoutes()
  {
    Routing.RegisterRoute(nameof(DiaryTodayPage), typeof(DiaryTodayPage));
    Routing.RegisterRoute(nameof(DiaryWeekPage), typeof(DiaryWeekPage));
    Routing.RegisterRoute(nameof(DiaryMonthPage), typeof(DiaryMonthPage));
  }
  private static void RegisterFoodSearchRoutes()
  {
    // Food Search -> Barcode Scanner
    Routing.RegisterRoute($"{nameof(FoodSearchPage)}" + $"/{nameof(BarcodeScannerPage)}", typeof(BarcodeScannerPage));
    // Food Search -> Food Diary Entry
    Routing.RegisterRoute($"{nameof(FoodSearchPage)}" + $"/{nameof(DiaryEntryFoodCreatePage)}", typeof(DiaryEntryFoodCreatePage));
    // Food Search -> Food View
    Routing.RegisterRoute($"{nameof(FoodSearchPage)}" + $"/{nameof(FoodViewPage)}", typeof(FoodViewPage));
    // Food Search -> Food Create
    Routing.RegisterRoute($"{nameof(FoodSearchPage)}" + $"/{nameof(FoodCreatePage)}", typeof(FoodCreatePage));
  }
  private static void RegisterWorkoutSearchRoutes()
  {
    // Workout Search -> Workout Create
    Routing.RegisterRoute($"{nameof(WorkoutSearchPage)}" + $"/{nameof(WorkoutCreatePage)}", typeof(WorkoutCreatePage));
    // Workout Search -> Workout Diary Entry
    Routing.RegisterRoute($"{nameof(WorkoutSearchPage)}" + $"/{nameof(DiaryEntryWorkoutCreatePage)}", typeof(DiaryEntryWorkoutCreatePage));
  }
  private static void RegisterFoodDetailsRoutes()
  {
    // Food Details -> Food Update
    Routing.RegisterRoute($"{nameof(FoodSearchPage)}" + $"/{nameof(FoodViewPage)}" + $"/{nameof(FoodUpdatePage)}", typeof(FoodUpdatePage));
    // Food Create -> User Search
    Routing.RegisterRoute($"{nameof(FoodCreatePage)}" + $"/{nameof(UserSearchPage)}", typeof(UserSearchPage));
    // Food Create -> Scan Barcode
    Routing.RegisterRoute($"{nameof(FoodCreatePage)}" + $"/{nameof(BarcodeScannerPage)}", typeof(BarcodeScannerPage));
  }
  private static void RegisterBodyMetricRoutes()
  {
    Routing.RegisterRoute($"{Routes.BodyMetricPage}/{Routes.BodyMetricCreatePage}", typeof(BodyMetricCreatePage));
    Routing.RegisterRoute($"{Routes.BodyMetricPage}/{Routes.BodyMetricViewPage}", typeof(BodyMetricViewPage));
  }
  private static void RegisterProfileRoutes()
  {
    Routing.RegisterRoute($"{Routes.UserProfilePage}/{Routes.UserProfileUpdatePage}", typeof(UserProfileUpdatePage));
  }
  private static void RegisterSettingsRoutes()
  {
    Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    Routing.RegisterRoute(nameof(SettingsDevPage), typeof(SettingsDevPage));
  }
}
