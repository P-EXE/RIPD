using RIPDApp.Views;

namespace RIPDApp.Config;

public static class ViewsConfig
{
  public static Task RegisterViews(this IServiceCollection services)
  {
    services.AddTransient<StatusBarV>();

    services.AddTransient<FoodListFoodV>();

    return Task.CompletedTask;
  }
}
