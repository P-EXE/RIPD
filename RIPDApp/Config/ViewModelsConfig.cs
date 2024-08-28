using RIPDApp.ViewModels;

namespace RIPDApp.Config;

public static class ViewModelsConfig
{
  public static Task RegisterViewModels(this IServiceCollection services)
  {
    services.AddTransient<StatusBarVM>();

    services.AddTransient<RegisterLoginVM>();

    services.AddTransient<HomeVM>();

    services.AddTransient<DiaryVM>();

    services.AddTransient<DiaryEntryVM>();

    services.AddTransient<FoodSearchVM>();
    services.AddTransient<WorkoutSearchVM>();
    services.AddTransient<UserSearchVM>();

    services.AddTransient<FoodListFoodVM>();

    services.AddTransient<FoodDetailsVM>();
    services.AddTransient<WorkoutDetailsVM>();

    services.AddTransient<BodyMetricVM>();
    services.AddTransient<BodyMetricDetailsVM>();

    services.AddTransient<FitnessTargetVM>();

    services.AddTransient<ScannerVM>();

    services.AddTransient<UserProfileVM>();

    services.AddTransient<SettingsVM>();
    services.AddTransient<SettingsDevVM>();

    return Task.CompletedTask;
  }
}
