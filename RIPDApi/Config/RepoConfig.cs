using RIPDApi.Repos;

namespace RIPDApi.Config;

public static class RepoConfig
{
  public static Task RegisterRepos(this IServiceCollection services)
  {
    services.AddTransient<IFoodRepo, FoodRepo>();
    services.AddTransient<IWorkoutRepo, WorkoutRepo>();
    services.AddTransient<IDiaryRepo, DiaryRepo>();
    services.AddTransient<IUserRepo, UserRepo>();

    return Task.CompletedTask;
  }
}
