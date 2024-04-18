using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/workouts")]
[ApiController]
public class WorkoutController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly SQLDataBaseContext _dbContext;

  public WorkoutController(IMapper mapper, SQLDataBaseContext dbContext)
  {
    _mapper = mapper;
    _dbContext = dbContext;
  }

  #region Create

  [HttpPost]
  public async Task CreateWorkoutAsync([FromBody] WorkoutDTO_Create createWorkout)
  {
    AppUser? contributer = await _dbContext.Users.FindAsync(createWorkout.ContributerId);
    Workout workout = new()
    {
      Name = createWorkout.Name,
      Description = createWorkout.Description,
      ContributerId = createWorkout.ContributerId,
      Contributer = contributer,
      Energy = createWorkout.Energy
    };

    await _dbContext.Workouts.AddAsync(workout);
    await _dbContext.SaveChangesAsync();
  }

  #endregion Create

  #region Read

  [HttpGet("{workoutId}")]
  public async Task<Workout?> GetWorkoutByIdAsync([FromRoute] string workoutId)
  {
    Workout? workout = await _dbContext.Workouts
      .FindAsync(workoutId);
    return workout;
  }

  #endregion Read
}
