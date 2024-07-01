using RIPDShared.Models;

namespace RIPDApp.Services;

public interface IWorkoutService
{
  Task<Workout?> CreateWorkoutAsync(Workout workout);
  Task<IEnumerable<Workout>?> GetWorkoutsByNameAtPositionAsync(string query, int position);
}
