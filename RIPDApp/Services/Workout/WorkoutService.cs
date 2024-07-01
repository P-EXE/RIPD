using AutoMapper;
using RIPDShared.Models;

namespace RIPDApp.Services;

public class WorkoutService : IWorkoutService
{
  private readonly IHttpService _httpService;
  private readonly IMapper _mapper;
  public WorkoutService(IHttpService httpService, IMapper mapper)
  {
    _httpService = httpService;
    _mapper = mapper;
  }

  // Needs AutoMapper support
  public async Task<Workout?> CreateWorkoutAsync(Workout workout)
  {
    // Mapping
    workout.Contributer = Statics.Auth.Owner;

    workout.ContributerId = Statics.Auth.Owner?.Id;

    // API
    Workout? retWorkout = await _httpService.PostAsync<Workout, Workout>("workout", workout);

    // Return
    return retWorkout;
  }

  public async Task<IEnumerable<Workout>?> GetWorkoutsByNameAtPositionAsync(string query, int position)
  {
    if (string.IsNullOrEmpty(query))
    {
      return null;
    }

    IEnumerable<Workout>? workouts;

    Dictionary<string, string> queries = new()
    {
      ["name"] = query,
      ["position"] = position.ToString(),
    };

    workouts = await _httpService.GetAsync<IEnumerable<Workout>?>("workout", queries);

    return workouts;
  }
}
