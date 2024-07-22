using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RIPDApi.Repos;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/workout")]
[ApiController]
public class WorkoutController : ControllerBase
{
  private readonly UserManager<AppUser> _userManager;
  private readonly IWorkoutRepo _workoutRepo;

  public WorkoutController(UserManager<AppUser> userManager, IWorkoutRepo workoutRepo)
  {
    _userManager = userManager;
    _workoutRepo = workoutRepo;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Workout?>> CreateWorkoutAsync([FromBody] Workout_Create createWorkout)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Workout? workout = null;

    if (createWorkout == null) return BadRequest(createWorkout);

    try
    {
      workout = await _workoutRepo.CreateWorkoutAsync(createWorkout);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    if (workout == null) return NotFound(workout);
    return Created(nameof(CreateWorkoutAsync), workout);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Workout?>> GetWorkoutByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Workout? workout = null;

    if (id == default) return BadRequest(id);

    workout = await _workoutRepo.ReadWorkoutByIdAsync(id);

    return workout == null ? NotFound(workout) : Ok(workout);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Workout>?>> GetWorkoutsByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    IEnumerable<Workout>? workouts = null;

    if (name == null) return BadRequest(name);

    workouts = await _workoutRepo.ReadWorkoutsByNameAtPositionAsync(name, position);

    return workouts!.Any() ? Ok(workouts) : NotFound(workouts);
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Workout?>> UpdateWorkoutAsync([FromBody] Workout_Update updateWorkout)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Workout? workout = null;

    if (updateWorkout == null) return BadRequest(workout);

    try
    {
      workout = await _workoutRepo.UpdateWorkoutAsync(updateWorkout);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return workout == null ? Conflict(updateWorkout) : Ok(workout);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<bool>> DeleteWorkoutByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    bool success = false;

    if (id == default) return BadRequest(id);

    try
    {
      success = await _workoutRepo.DeleteWorkoutByIdAsync(id);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return success ? Ok(success) : NotFound(id);
  }
}
