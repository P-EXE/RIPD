using RIPDApp.Pages;

namespace RIPDApp.Config;

public static class PagesConfig
{
  public static Task RegisterPages(this MauiAppBuilder builder)
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

    builder.Services.AddTransient<FitnessTargetPage>();

    builder.Services.AddTransient<BarcodeScannerPage>();

    builder.Services.AddTransient<UserProfilePage>();
    builder.Services.AddScoped<UserProfileUpdatePage>();

    builder.Services.AddTransient<SettingsPage>();
    builder.Services.AddTransient<SettingsDevPage>();

    return Task.CompletedTask;
  }
}
