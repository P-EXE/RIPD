using RIPDShared.Models;

namespace RIPDApp.Services;

public class WorkoutServiceMock : IWorkoutService
{
  public Task<Workout?> CreateWorkoutAsync(Workout workout)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Workout>?> GetWorkoutsByNameAtPositionAsync(string query, int position)
  {
    throw new NotImplementedException();
  }
}
