using RIPDApp.ViewModels;

namespace RIPDApp.Config;

public static class ViewModelsConfig
{
  public static Task RegisterViewModels(this MauiAppBuilder builder)
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

    builder.Services.AddTransient<FitnessTargetVM>();

    builder.Services.AddTransient<ScannerVM>();

    builder.Services.AddTransient<UserProfileVM>();

    builder.Services.AddTransient<SettingsVM>();
    builder.Services.AddTransient<SettingsDevVM>();

    return Task.CompletedTask;
  }
}
