using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDApi.Services;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/user/{userId}/diary")]
[ApiController]
public class DiaryController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly SQLDataBaseContext _sqlContext;
  private readonly MongoDataBaseContext _mongoContext;
  private MongoDBService _mongoDBService;

  public DiaryController(IMapper mapper, SQLDataBaseContext sqlContext, MongoDataBaseContext mongoContext, MongoDBService mongoDBService)
  {
    _mapper = mapper;
    _sqlContext = sqlContext;
    _mongoContext = mongoContext;
    _mongoDBService = mongoDBService;
  }

  #region Create

  [HttpPost("foods")]
  public async Task AddFoodEntryAsync([FromRoute] Guid userId, Food_DiaryEntryDTO_Create createDiaryFood)
  {
    AppUser? user = await _sqlContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
    Food? food = await _sqlContext.Foods.FindAsync(createDiaryFood.FoodId);

    int lastEntryNr = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.FoodEntries)
      .Where(u => u.Id == userId).First()
      .Diary
      .FoodEntries.Select(f => (int?)f.EntryNr).Max() ?? 0;

    Food_DiaryEntry diaryFood = new()
    {
      DiaryId = userId,
      Diary = user.Diary,
      EntryNr = lastEntryNr + 1,
      FoodId = createDiaryFood.FoodId,
      Food = food,
      Amount = createDiaryFood.Amount,
      Acted = createDiaryFood.Acted,
      Added = DateTime.UtcNow
    };

    user.Diary.FoodEntries.Add(diaryFood);
    await _sqlContext.SaveChangesAsync();
  }

  [HttpPost("workouts")]
  public async Task AddWorkoutEntryAsync([FromRoute] Guid userId, Workout_DiaryEntryDTO_Create createDiaryWorkout)
  {
    AppUser? user = await _sqlContext.Users.FindAsync(userId);
    Workout? workout = await _sqlContext.Workouts.FindAsync(createDiaryWorkout.WorkoutId);

    int lastEntryNr = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.WorkoutEntries)
      .Where(u => u.Id == userId).First()
      .Diary
      .WorkoutEntries.Select(w => (int?)w.EntryNr).Max() ?? 0;

    Workout_DiaryEntry diaryWorkout = new()
    {
      DiaryId = userId,
      Diary = user.Diary,
      EntryNr = lastEntryNr + 1,
      WorkoutId = createDiaryWorkout.WorkoutId,
      Workout = workout,
      Amount = createDiaryWorkout.Amount,
      Acted = createDiaryWorkout.Acted,
      Added = createDiaryWorkout.Added
    };

    user.Diary.WorkoutEntries.Add(diaryWorkout);
    await _sqlContext.SaveChangesAsync();
  }

  [HttpPost("runs")]
  public async Task AddRunEntryAsync([FromRoute] Guid userId, Run_DiaryEntryDTO_Create createDiaryRun)
  {
    AppUser? user = await _sqlContext.Users.FindAsync(userId);

    int lastEntryNr = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.RunEntries)
      .Where(u => u.Id == userId).First()
      .Diary
      .RunEntries.Select(w => (int?)w.EntryNr).Max() ?? 0;

    Run_DiaryEntry diaryRun = new()
    {
      DiaryId = userId,
      Diary = user.Diary,
      EntryNr = lastEntryNr + 1,
      MongoDBId = "No MongoDB",
      Acted = createDiaryRun.Acted,
      Added = createDiaryRun.Added
    };

    Run run = new()
    {
      Locations = createDiaryRun.Locations
    };

    await _mongoDBService.SaveToMongoDBAsync("Runs", run);

    user.Diary.RunEntries.Add(diaryRun);
    await _sqlContext.SaveChangesAsync();
  }

  #endregion Create

  #region Read

  [HttpGet("foods/recent")]
  public async Task<IEnumerable<Food?>> GetRecentFoodEntriesAsync([FromRoute] Guid userId)
  {
    IEnumerable<Food?> foods = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.FoodEntries).ThenInclude(f => f.Food)
      .Where(u => u.Id.Equals(userId)).First()
      .Diary
      .FoodEntries
      .OrderByDescending(f => f.Acted).Take(20).Select(f => f.Food).AsEnumerable();
    return foods;
  }

  [HttpGet("foods/{date}")]
  public async Task<IEnumerable<Food?>> GetFoodEntriesByDateAsync([FromRoute] Guid userId, [FromRoute] DateTime date)
  {
    IEnumerable<Food?> foods = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.FoodEntries).ThenInclude(f => f.Food)
      .Where(u => u.Id.Equals(userId)).First()
      .Diary
      .FoodEntries
      .Where(f => f.Acted.Date.Equals(date.Date)).Select(f => f.Food).AsEnumerable();
    return foods;
  }

  [HttpGet("runs/{entryNr}")]
  public async Task<(Run_DiaryEntry?, Run?)?> GetRunByIdAsync([FromRoute] Guid userId, [FromRoute] int entryNr)
  {
    Run_DiaryEntry? runEntry = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.RunEntries)
      .Where(u => u.Id.Equals(userId)).First()
      .Diary
      .RunEntries
      .Where(r => r.EntryNr == entryNr).FirstOrDefault();
    Run run = await _mongoDBService.GetFromMongoDBAsync<Run>("Runs", runEntry.MongoDBId);
    (Run_DiaryEntry, Run) ret = (runEntry, run);
    return ret;
  }

  #endregion Read

  #region Update

  [HttpPatch("foods/{foodEntryNr}")]
  public async Task UpdateFoodEntryById([FromRoute] Guid userId, [FromRoute] int foodEntryNr, Food_DiaryEntryDTO_Update updateDiaryFood)
  {
    Food_DiaryEntry? foodEntry = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.FoodEntries)
      .Where(u => u.Id == userId).FirstOrDefault()
      .Diary
      .FoodEntries
      .Where(f => f.EntryNr == foodEntryNr).FirstOrDefault();

    foodEntry.Amount = updateDiaryFood.Amount;
    foodEntry.Acted = updateDiaryFood.Added;

    _sqlContext.Update(foodEntry);
    await _sqlContext.SaveChangesAsync();
  }

  #endregion Update

  #region Delete

  [HttpDelete("foods/{entryNr}")]
  public async Task DeleteFoodEntryByEntryNr([FromRoute] Guid userId, [FromRoute] int entryNr)
  {
    Food_DiaryEntry? foodEntry = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.FoodEntries)
      .Where(u => u.Id == userId).First()
      .Diary
      .FoodEntries
      .Where(fe => fe.EntryNr == entryNr).FirstOrDefault();

    _sqlContext.Remove(foodEntry);
    await _sqlContext.SaveChangesAsync();
  }

  [HttpDelete("runs/{entryNr}")]
  public async Task DeleteRunEntryByEntryNr([FromRoute] Guid userId, [FromRoute] int entryNr)
  {
    Run_DiaryEntry? runEntry = _sqlContext.Users
      .Include(u => u.Diary).ThenInclude(d => d.RunEntries)
      .Where(u => u.Id == userId).First()
      .Diary
      .RunEntries
      .Where(re => re.EntryNr == entryNr).FirstOrDefault();

    _sqlContext.Remove(runEntry);

    await _mongoDBService.DeleteFromMongoDBAsync<Run>("Runs", runEntry.MongoDBId);
    await _sqlContext.SaveChangesAsync();
  }

  #endregion Delete
}
