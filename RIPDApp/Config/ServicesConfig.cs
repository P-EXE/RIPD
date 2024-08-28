using RIPDApp.Services;
using System.Net.Http.Headers;

namespace RIPDApp.Config;

public static class ServicesConfig
{
  public static Task RegisterServices(this IServiceCollection services)
  {
    services.AddTransient<IHttpService, HttpService>();
    services.AddHttpClient<IHttpService, HttpService>(options =>
    {
      options.BaseAddress = new(Statics.API.RouteBaseHttp);
      options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Auth.BearerToken?.AccessToken ?? "");
    });

    services.AddTransient<IOwnerService, OwnerService>();
    services.AddTransient<IFoodService, FoodService>();
    services.AddTransient<IWorkoutService, WorkoutService>();
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IDiaryService, DiaryService>();

    return Task.CompletedTask;
  }

  public static Task RegisterMockServices(this IServiceCollection services)
  {
    services.AddTransient<IOwnerService, OwnerServiceMock>();
    services.AddTransient<IFoodService, FoodServiceMock>();
    services.AddTransient<IWorkoutService, WorkoutServiceMock>();
    services.AddTransient<IUserService, UserServiceMock>();
    services.AddTransient<IDiaryService, DiaryServiceMock>();

    return Task.CompletedTask;
  }
}
