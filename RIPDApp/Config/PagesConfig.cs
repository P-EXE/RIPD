using RIPDApp.Pages;

namespace RIPDApp.Config;

public static class PagesConfig
{
  public static Task RegisterPages(this IServiceCollection services)
  {
    services.AddScoped<AutoLoginPage>();
    services.AddTransient<RegisterPage>();
    services.AddTransient<LoginPage>();

    services.AddTransient<HomePage>();
    services.AddTransient<RunPage>();

    services.AddTransient<DiaryTodayPage>();
    services.AddTransient<DiaryWeekPage>();
    services.AddTransient<DiaryMonthPage>();

    services.AddTransient<DiaryEntryFoodCreatePage>();
    services.AddTransient<DiaryEntryFoodEditPage>();
    services.AddTransient<DiaryEntryWorkoutCreatePage>();
    services.AddTransient<DiaryEntryWorkoutEditPage>();

    services.AddTransient<FoodSearchPage>();
    services.AddTransient<WorkoutSearchPage>();
    services.AddTransient<UserSearchPage>();

    services.AddTransient<FoodDetailsPage>();
    services.AddTransient<FoodCreatePage>();
    services.AddTransient<FoodUpdatePage>();
    services.AddTransient<FoodViewPage>();

    services.AddTransient<WorkoutCreatePage>();

    services.AddTransient<BodyMetricPage>();
    services.AddTransient<BodyMetricCreatePage>();
    services.AddTransient<BodyMetricViewPage>();

    services.AddTransient<FitnessTargetPage>();

    services.AddTransient<BarcodeScannerPage>();

    services.AddTransient<UserProfilePage>();
    services.AddScoped<UserProfileUpdatePage>();

    services.AddTransient<SettingsPage>();
    services.AddTransient<SettingsDevPage>();

    return Task.CompletedTask;
  }
}
