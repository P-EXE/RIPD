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
  public async Task<Workout?> CreateWorkoutAsync([FromBody] Workout_Create createWorkout)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _workoutRepo.CreateWorkoutAsync(createWorkout);
  }

  [HttpGet("{id}")]
  public async Task<Workout?> GetWorkoutByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _workoutRepo.ReadWorkoutByIdAsync(id);
  }

  [HttpGet]
  public async Task<IEnumerable<Workout>?> GetWorkoutsByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _workoutRepo.ReadWorkoutsByNameAtPositionAsync(name, position);
  }

  [HttpPut]
  public async Task<Workout?> UpdateWorkoutAsync([FromBody] Workout_Update updateWorkout)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _workoutRepo.UpdateWorkoutAsync(updateWorkout);
  }

  [HttpDelete("{id}")]
  public async Task<bool> DeleteWorkoutByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _workoutRepo.DeleteWorkoutByIdAsync(id);
  }
}
