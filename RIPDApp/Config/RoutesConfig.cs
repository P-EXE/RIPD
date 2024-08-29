using RIPDApp.Pages;

namespace RIPDApp.Config;

public static class RoutesConfig
{
  public static Task RegisterRoutes()
  {
    RegisterAuthRoutes();
    RegisterHomeRoutes();
    RegisterDiaryRoutes();
    RegisterFoodSearchRoutes();
    RegisterWorkoutSearchRoutes();
    RegisterFoodDetailsRoutes();
    RegisterBodyMetricRoutes();
    RegisterProfileRoutes();
    RegisterSettingsRoutes();

    return Task.CompletedTask;
  }

  private static Task RegisterAuthRoutes()
  {
    Routing.RegisterRoute(Routes.AutoLoginPage, typeof(AutoLoginPage));
    Routing.RegisterRoute(Routes.RegisterPage, typeof(RegisterPage));
    Routing.RegisterRoute(Routes.LoginPage, typeof(LoginPage));

    return Task.CompletedTask;
  }

  private static Task RegisterHomeRoutes()
  {
    Routing.RegisterRoute(Routes.HomePage + $"/{Routes.RunPage}", typeof(RunPage));

    return Task.CompletedTask;
  }

  private static Task RegisterDiaryRoutes()
  {
    Routing.RegisterRoute(Routes.DiaryTodayPage + $"/{Routes.DiaryEntryFoodEditPage}", typeof(DiaryEntryFoodEditPage));
    Routing.RegisterRoute(Routes.DiaryTodayPage + $"/{Routes.DiaryEntryWorkoutEditPage}", typeof(DiaryEntryWorkoutEditPage));
    Routing.RegisterRoute(Routes.DiaryWeekPage + $"/{Routes.DiaryEntryFoodEditPage}", typeof(DiaryEntryFoodEditPage));
    Routing.RegisterRoute(Routes.DiaryWeekPage + $"/{Routes.DiaryEntryWorkoutEditPage}", typeof(DiaryEntryWorkoutEditPage));
    Routing.RegisterRoute(Routes.DiaryMonthPage + $"/{Routes.DiaryEntryFoodEditPage}", typeof(DiaryEntryFoodEditPage));
    Routing.RegisterRoute(Routes.DiaryMonthPage + $"/{Routes.DiaryEntryWorkoutEditPage}", typeof(DiaryEntryWorkoutEditPage));

    return Task.CompletedTask;
  }

  private static Task RegisterFoodSearchRoutes()
  {
    Routing.RegisterRoute($"{Routes.FoodSearchPage}" + $"/{Routes.BarcodeScannerPage}", typeof(BarcodeScannerPage));
    Routing.RegisterRoute($"{Routes.FoodSearchPage}" + $"/{Routes.DiaryEntryFoodCreatePage}", typeof(DiaryEntryFoodCreatePage));
    Routing.RegisterRoute($"{Routes.FoodSearchPage}" + $"/{Routes.FoodViewPage}", typeof(FoodViewPage));
    Routing.RegisterRoute($"{Routes.FoodSearchPage}" + $"/{Routes.FoodCreatePage}", typeof(FoodCreatePage));

    return Task.CompletedTask;
  }

  private static Task RegisterWorkoutSearchRoutes()
  {
    Routing.RegisterRoute($"{Routes.WorkoutSearchPage}" + $"/{Routes.WorkoutCreatePage}", typeof(WorkoutCreatePage));
    Routing.RegisterRoute($"{Routes.WorkoutSearchPage}" + $"/{Routes.DiaryEntryWorkoutCreatePage}", typeof(DiaryEntryWorkoutCreatePage));

    return Task.CompletedTask;
  }

  private static Task RegisterFoodDetailsRoutes()
  {
    Routing.RegisterRoute($"{Routes.FoodSearchPage}" + $"/{Routes.FoodViewPage}" + $"/{Routes.FoodUpdatePage}", typeof(FoodUpdatePage));
    Routing.RegisterRoute($"{Routes.FoodCreatePage}" + $"/{Routes.UserSearchPage}", typeof(UserSearchPage));
    Routing.RegisterRoute($"{Routes.FoodCreatePage}" + $"/{Routes.BarcodeScannerPage}", typeof(BarcodeScannerPage));

    return Task.CompletedTask;
  }

  private static Task RegisterBodyMetricRoutes()
  {
    Routing.RegisterRoute($"{Routes.BodyMetricPage}/{Routes.BodyMetricCreatePage}", typeof(BodyMetricCreatePage));
    Routing.RegisterRoute($"{Routes.BodyMetricPage}/{Routes.BodyMetricViewPage}", typeof(BodyMetricViewPage));

    return Task.CompletedTask;
  }

  private static Task RegisterProfileRoutes()
  {
    Routing.RegisterRoute($"{Routes.UserProfilePage}/{Routes.UserProfileUpdatePage}", typeof(UserProfileUpdatePage));

    return Task.CompletedTask;
  }

  private static Task RegisterSettingsRoutes()
  {
    Routing.RegisterRoute(Routes.SettingsPage, typeof(SettingsPage));
    Routing.RegisterRoute(Routes.SettingsDevPage, typeof(SettingsDevPage));

    return Task.CompletedTask;
  }
}
