using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RIPDApi.Repos;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

/// <summary>
/// Controller for all the major CRUD operations concerning the Diary
/// Middleware and Auth stuff here
/// </summary>
[Route("api/diary")]
[ApiController]
public class DiaryController : ControllerBase
{
  private readonly UserManager<AppUser> _userManager;
  private readonly IDiaryRepo _diaryRepo;

  public DiaryController(UserManager<AppUser> userManager, IDiaryRepo diaryRepo)
  {
    _userManager = userManager;
    _diaryRepo = diaryRepo;
  }

  #region Create
  [HttpPost("food")]
  public async Task<DiaryEntry_Food?> AddFoodEntryAsync(DiaryEntry_Food_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.CreateFoodEntryAsync(createEntry);
  }

  [HttpPost("workout")]
  public async Task<DiaryEntry_Workout?> AddWorkoutEntryAsync(DiaryEntry_Workout_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.CreateWorkoutEntryAsync(createEntry);
  }

  [HttpPost("run")]
  public async Task<DiaryEntry_Run?> AddRunEntryAsync(DiaryEntry_Run_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.CreateRunEntryAsync(createEntry);
  }
  #endregion Create

  #region Read
  [HttpGet("{diaryId}/food")]
  public async Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesFromToDate([FromRoute] Guid diaryId, [FromQuery] DateTime start, [FromQuery] DateTime end)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.ReadFoodEntriesFromToDateAsync(diaryId, start, end);
  }

  [HttpGet("{diaryId}/workout")]
  public async Task<IEnumerable<DiaryEntry_Workout>?> GetWorkoutEntriesFromToDate([FromRoute] Guid diaryId, [FromQuery] DateTime start, [FromQuery] DateTime end)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.ReadWorkoutEntriesFromToDateAsync(diaryId, start, end);
  }

  [HttpGet("{diaryId}/run")]
  public async Task<IEnumerable<DiaryEntry_Run>?> GetRunEntriesFromToDate([FromRoute] Guid diaryId, [FromQuery] DateTime start, [FromQuery] DateTime end)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.ReadRunEntriesFromToDateAsync(diaryId, start, end);
  }
  #endregion Read

  #region Update
  [HttpPut("food")]
  public async Task<DiaryEntry_Food?> UpdateFoodEntryAsync([FromBody] DiaryEntry_Food_Update updateEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.UpdateFoodEntryAsync(updateEntry);
  }
  [HttpPut("workout")]
  public async Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync([FromBody] DiaryEntry_Workout_Update updateEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.UpdateWorkoutEntryAsync(updateEntry);
  }
  [HttpPut("run")]
  public async Task<DiaryEntry_Run?> UpdateRunEntryAsync([FromBody] DiaryEntry_Run_Update updateEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.UpdateRunEntryAsync(updateEntry);
  }
  #endregion Update

  #region Delete
  [HttpDelete("{diaryId}/food/{entryId}")]
  public async Task<bool> DeleteFoodEntryAsync([FromRoute] Guid diaryId, [FromRoute] int entryId)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.DeleteFoodEntryAsync(diaryId, entryId);
  }
  [HttpDelete("{diaryId}/workout/{entryId}")]
  public async Task<bool> DeleteWorkoutEntryAsync([FromRoute] Guid diaryId, [FromRoute] int entryId)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.DeleteWorkoutEntryAsync(diaryId, entryId);
  }
  [HttpDelete("{diaryId}/run/{entryId}")]
  public async Task<bool> DeleteRunEntryAsync([FromRoute] Guid diaryId, [FromRoute] int entryId)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.DeleteRunEntryAsync(diaryId, entryId);
  }
  #endregion Delete
}
