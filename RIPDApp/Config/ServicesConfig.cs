using RIPDApp.Services;
using System.Net.Http.Headers;

namespace RIPDApp.Config;

public static class ServicesConfig
{
  public static Task RegisterServices(this MauiAppBuilder builder)
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

    return Task.CompletedTask;
  }

  public static Task RegisterMockServices(this MauiAppBuilder builder)
  {
    builder.Services.AddTransient<IOwnerService, OwnerServiceMock>();
    builder.Services.AddTransient<IFoodService, FoodServiceMock>();
    builder.Services.AddTransient<IWorkoutService, WorkoutServiceMock>();
    builder.Services.AddTransient<IUserService, UserServiceMock>();
    builder.Services.AddTransient<IDiaryService, DiaryServiceMock>();

    return Task.CompletedTask;
  }
}
