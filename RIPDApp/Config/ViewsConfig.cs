using RIPDApp.Views;

namespace RIPDApp.Config;

public static class ViewsConfig
{
  public static Task RegisterViews(this MauiAppBuilder builder)
  {
    builder.Services.AddTransient<StatusBarV>();

    builder.Services.AddTransient<FoodListFoodV>();

    return Task.CompletedTask;
  }
}
