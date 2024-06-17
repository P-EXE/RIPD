using Microsoft.AspNetCore.Authorization;
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
  [HttpPost("food"), Authorize]
  public async Task<DiaryEntry_Food?> AddFoodEntryAsync(DiaryEntry_Food_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.CreateFoodEntryAsync(createEntry);
  }

  [HttpPost("workout"), Authorize]
  public async Task<DiaryEntry_Workout?> AddWorkoutEntryAsync(DiaryEntry_Workout_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.CreateWorkoutEntryAsync(createEntry);
  }

  [HttpPost("run"), Authorize]
  public async Task<DiaryEntry_Run?> AddRunEntryAsync(DiaryEntry_Run_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.CreateRunEntryAsync(createEntry);
  }
  #endregion Create

  #region Read
  [HttpGet("food"), Authorize]
  public async Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntries([FromQuery] string? diary = null, [FromQuery] DateTime start = default, [FromQuery] DateTime end = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);

    Guid diaryId = diary == null ? user.Id : new(diary);
    DateTime startDate = start == default ? DateTime.MinValue : start;
    DateTime endDate = end == default ? DateTime.Now : end;

    return await _diaryRepo.ReadFoodEntriesFromToDateAsync(diaryId, startDate, endDate);
  }

  [HttpGet("workout"), Authorize]
  public async Task<IEnumerable<DiaryEntry_Workout>?> GetWorkoutEntriesFromToDate([FromQuery] string? diary = null, [FromQuery] DateTime start = default, [FromQuery] DateTime end = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);

    Guid diaryId = diary == null ? user.Id : new(diary);
    DateTime startDate = start == default ? DateTime.MinValue : start;
    DateTime endDate = end == default ? DateTime.Now : end;

    return await _diaryRepo.ReadWorkoutEntriesFromToDateAsync(diaryId, startDate, endDate);
  }

  [HttpGet("run"), Authorize]
  public async Task<IEnumerable<DiaryEntry_Run>?> GetRunEntriesFromToDate([FromQuery] string? diary = null, [FromQuery] DateTime start = default, [FromQuery] DateTime end = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);

    Guid diaryId = diary == null ? user.Id : new(diary);
    DateTime startDate = start == default ? DateTime.MinValue : start;
    DateTime endDate = end == default ? DateTime.Now : end;

    return await _diaryRepo.ReadRunEntriesFromToDateAsync(diaryId, startDate, endDate);
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
  [HttpDelete("food")]
  public async Task<bool> DeleteFoodEntryAsync([FromQuery] Guid diary, [FromQuery] int entry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.DeleteFoodEntryAsync(diary, entry);
  }
  [HttpDelete("workout")]
  public async Task<bool> DeleteWorkoutEntryAsync([FromQuery] Guid diary, [FromQuery] int entry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.DeleteWorkoutEntryAsync(diary, entry);
  }
  [HttpDelete("run")]
  public async Task<bool> DeleteRunEntryAsync([FromQuery] Guid diary, [FromQuery] int entry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _diaryRepo.DeleteRunEntryAsync(diary, entry);
  }
  #endregion Delete
}
