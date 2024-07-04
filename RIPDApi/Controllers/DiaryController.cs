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
  public async Task<ActionResult<DiaryEntry_Food?>> AddFoodEntryAsync(DiaryEntry_Food_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    DiaryEntry_Food? diaryEntry = null;

    if (createEntry == null) return BadRequest(createEntry);

    try
    {
      diaryEntry = await _diaryRepo.CreateFoodEntryAsync(createEntry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntry == null ? NotFound(diaryEntry) : Created(nameof(AddFoodEntryAsync), diaryEntry);
  }

  [HttpPost("workout"), Authorize]
  public async Task<ActionResult<DiaryEntry_Workout?>> AddWorkoutEntryAsync(DiaryEntry_Workout_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    DiaryEntry_Workout? diaryEntry = null;

    if (createEntry == null) return BadRequest(createEntry);

    try
    {
      diaryEntry = await _diaryRepo.CreateWorkoutEntryAsync(createEntry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntry == null ? NotFound(diaryEntry) : Created(nameof(AddWorkoutEntryAsync), diaryEntry);
  }

  [HttpPost("run"), Authorize]
  public async Task<ActionResult<DiaryEntry_Run?>> AddRunEntryAsync(DiaryEntry_Run_Create createEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    DiaryEntry_Run? diaryEntry = null;

    if (createEntry == null) return BadRequest(createEntry);

    try
    {
      diaryEntry = await _diaryRepo.CreateRunEntryAsync(createEntry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntry == null ? NotFound(diaryEntry) : Created(nameof(AddWorkoutEntryAsync), diaryEntry);
  }
  #endregion Create

  #region Read
  [HttpGet("food"), Authorize]
  public async Task<ActionResult<IEnumerable<DiaryEntry_Food>?>> GetFoodEntries([FromQuery] string? diary = null, [FromQuery] DateTime start = default, [FromQuery] DateTime end = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    IEnumerable<DiaryEntry_Food>? diaryEntries = null;

    Guid diaryId = diary == null ? user.Id : new(diary);
    DateTime startDate = start == default ? DateTime.MinValue : start;
    DateTime endDate = end == default ? DateTime.Now : end;

    try
    {
      diaryEntries = await _diaryRepo.ReadFoodEntriesFromToDateAsync(diaryId, startDate, endDate);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntries == null ? NotFound(diaryEntries) : Ok(diaryEntries);
  }

  [HttpGet("workout"), Authorize]
  public async Task<ActionResult<IEnumerable<DiaryEntry_Workout>?>> GetWorkoutEntriesFromToDate([FromQuery] string? diary = null, [FromQuery] DateTime start = default, [FromQuery] DateTime end = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    IEnumerable<DiaryEntry_Workout>? diaryEntries = null;

    Guid diaryId = diary == null ? user.Id : new(diary);
    DateTime startDate = start == default ? DateTime.MinValue : start;
    DateTime endDate = end == default ? DateTime.Now : end;

    try
    {
      diaryEntries = await _diaryRepo.ReadWorkoutEntriesFromToDateAsync(diaryId, startDate, endDate);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntries == null ? NotFound(diaryEntries) : Ok(diaryEntries);
  }

  [HttpGet("run"), Authorize]
  public async Task<ActionResult<IEnumerable<DiaryEntry_Run>?>> GetRunEntriesFromToDate([FromQuery] string? diary = null, [FromQuery] DateTime start = default, [FromQuery] DateTime end = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    IEnumerable<DiaryEntry_Run>? diaryEntries = null;

    Guid diaryId = diary == null ? user.Id : new(diary);
    DateTime startDate = start == default ? DateTime.MinValue : start;
    DateTime endDate = end == default ? DateTime.Now : end;

    try
    {
      diaryEntries = await _diaryRepo.ReadRunEntriesFromToDateAsync(diaryId, startDate, endDate);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntries == null ? NotFound(diaryEntries) : Ok(diaryEntries);
  }
  #endregion Read

  #region Update
  [HttpPut("food")]
  public async Task<ActionResult<DiaryEntry_Food?>> UpdateFoodEntryAsync([FromBody] DiaryEntry_Food_Update updateEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    DiaryEntry_Food? diaryEntry = null;

    if (updateEntry == null) return BadRequest(updateEntry);

    try
    {
      diaryEntry = await _diaryRepo.UpdateFoodEntryAsync(updateEntry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntry == null ? Conflict(updateEntry) : Ok(diaryEntry);
  }
  [HttpPut("workout")]
  public async Task<ActionResult<DiaryEntry_Workout?>> UpdateWorkoutEntryAsync([FromBody] DiaryEntry_Workout_Update updateEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    DiaryEntry_Workout? diaryEntry = null;

    if (updateEntry == null) return BadRequest(updateEntry);

    try
    {
      diaryEntry = await _diaryRepo.UpdateWorkoutEntryAsync(updateEntry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntry == null ? Conflict(updateEntry) : Ok(diaryEntry);
  }
  [HttpPut("run")]
  public async Task<ActionResult<DiaryEntry_Run?>> UpdateRunEntryAsync([FromBody] DiaryEntry_Run_Update updateEntry)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    DiaryEntry_Run? diaryEntry = null;

    if (updateEntry == null) return BadRequest(updateEntry);

    try
    {
      diaryEntry = await _diaryRepo.UpdateRunEntryAsync(updateEntry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return diaryEntry == null ? Conflict(updateEntry) : Ok(diaryEntry);
  }
  #endregion Update

  #region Delete
  [HttpDelete("food")]
  public async Task<ActionResult> DeleteFoodEntryAsync([FromQuery] int entry, [FromQuery] string? diary = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Guid diaryId = diary == null ? user.Id : new(diary);
    bool success = false;

    if (entry == default) return BadRequest(entry);

    try
    {
      success = await _diaryRepo.DeleteFoodEntryAsync(diaryId, entry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return success ? Ok(entry) : NotFound(entry);
  }
  [HttpDelete("workout")]
  public async Task<ActionResult> DeleteWorkoutEntryAsync([FromQuery] int entry, [FromQuery] string? diary = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Guid diaryId = diary == null ? user.Id : new(diary);
    bool success = false;

    if (entry == default) return BadRequest(entry);

    try
    {
      success = await _diaryRepo.DeleteWorkoutEntryAsync(diaryId, entry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return success ? Ok(entry) : NotFound(entry);
  }
  [HttpDelete("run")]
  public async Task<ActionResult> DeleteRunEntryAsync([FromQuery] int entry, [FromQuery] string? diary = default)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Guid diaryId = diary == null ? user.Id : new(diary);
    bool success = false;

    if (entry == default) return BadRequest(entry);

    try
    {
      success = await _diaryRepo.DeleteRunEntryAsync(diaryId, entry);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return success ? Ok(entry) : NotFound(entry);
  }
  #endregion Delete
}
