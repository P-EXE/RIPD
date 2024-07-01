using AutoMapper;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Repos;

public class WorkoutRepo : IWorkoutRepo
{
  private readonly SQLDataBaseContext _sqlContext;
  private readonly IMapper _mapper;

  // Determines the ammount of Items returned via the Take method
  const int takeSize = 20;

  public WorkoutRepo(SQLDataBaseContext sqlContext, IMapper mapper)
  {
    _sqlContext = sqlContext;
    _mapper = mapper;
  }

  public async Task<Workout?> CreateWorkoutAsync(Workout_Create createWorkout)
  {
    // Mapping
    Workout workout = _mapper.Map<Workout>(createWorkout);

    // SQL Context
    await _sqlContext.Workouts.AddAsync(workout);
    await _sqlContext.SaveChangesAsync();

    // Return
    return workout;
  }

  public async Task<Workout?> ReadWorkoutByIdAsync(Guid id)
  {
    // SQL Context
    Workout? workout = await _sqlContext.Workouts.FindAsync(id);

    // Return
    return workout;
  }

  // Notice: Not awaiting anything
  public async Task<IEnumerable<Workout>?> ReadWorkoutsByNameAtPositionAsync(string name, int position)
  {
    // SQL Context
    // Null reference dereference !!!
    IEnumerable<Workout>? workouts = _sqlContext.Workouts
      .Where(w => w.Name.StartsWith(name))
      .Skip(position * takeSize)
      .Take(takeSize)
      .AsEnumerable();

    // Return
    return workouts;
  }

  public async Task<Workout?> UpdateWorkoutAsync(Workout_Update updateWorkout)
  {
    // Mapping
    Workout workout = _mapper.Map<Workout>(updateWorkout);

    // SQL Context
    _sqlContext.Workouts.Update(workout);
    await _sqlContext.SaveChangesAsync();

    // Return
    return workout;
  }

  public async Task<bool> DeleteWorkoutByIdAsync(Guid id)
  {
    // SQL Context
    // Possible null reference !!!
    Workout? workout = await _sqlContext.Workouts.FindAsync(id);
    _sqlContext.Workouts.Remove(workout);
    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }
}
