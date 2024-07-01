using RIPDShared.Models;

namespace RIPDApi.Repos;

public interface IWorkoutRepo
{
  Task<Workout?> CreateWorkoutAsync(Workout_Create createWorkout);
  Task<Workout?> ReadWorkoutByIdAsync(Guid id);
  Task<IEnumerable<Workout>?> ReadWorkoutsByNameAtPositionAsync(string name, int position);
  Task<Workout?> UpdateWorkoutAsync(Workout_Update updateFood);
  Task<bool> DeleteWorkoutByIdAsync(Guid id);
}
