using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDApi.Services;
using RIPDShared.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RIPDApi.Repos;

public class DiaryRepo : IDiaryRepo
{
  private readonly SQLDataBaseContext _sqlContext;
  private readonly MongoDBService _mongoService;
  private readonly IMapper _mapper;

  // Determines the ammount of Items returned via the Take method
  const int takeSize = 20;

  public DiaryRepo(SQLDataBaseContext sqlContext, MongoDBService mongoService, IMapper mapper)
  {
    _sqlContext = sqlContext;
    _mapper = mapper;
    _mongoService = mongoService;
  }

  #region Create
  public async Task<DiaryEntry_Food?> CreateFoodEntryAsync(DiaryEntry_Food_Create createFood)
  {
    // Mapping
    DiaryEntry_Food foodEntry = _mapper.Map<DiaryEntry_Food>(createFood);

    // SQL Context
    ICollection<DiaryEntry_Food> foodEntries = _sqlContext.Diaries
      .Include(d => d.FoodEntries)
      .First(d => d.OwnerId == createFood.DiaryId)
      .FoodEntries;

    foodEntries.Add(foodEntry);

    await _sqlContext.SaveChangesAsync();

    // Return
    return foodEntry;
  }

  public async Task<DiaryEntry_Workout?> CreateWorkoutEntryAsync(DiaryEntry_Workout_Create createWorkout)
  {
    // Mapping
    DiaryEntry_Workout workoutEntry = _mapper.Map<DiaryEntry_Workout>(createWorkout);

    // SQL Context
    ICollection<DiaryEntry_Workout> workoutEntries = _sqlContext.Diaries
      .Include(d => d.WorkoutEntries)
      .First(d => d.OwnerId == createWorkout.DiaryId)
      .WorkoutEntries;

    workoutEntries.Add(workoutEntry);

    await _sqlContext.SaveChangesAsync();

    // Return
    return workoutEntry;
  }

  public async Task<DiaryEntry_BodyMetric?> CreateBodyMetricEntryAsync(DiaryEntry_BodyMetric_Create create)
  {
    // Mapping
    DiaryEntry_BodyMetric entry = _mapper.Map<DiaryEntry_BodyMetric>(create);
    entry.EntryNr = 0;

    // SQL Context
    ICollection<DiaryEntry_BodyMetric> entries = _sqlContext.Diaries
      .Include(d => d.BodyMetrics)
      .First(d => d.OwnerId == create.DiaryId)
      .BodyMetrics;

    entries.Add(entry);

    await _sqlContext.SaveChangesAsync();

    // Return
    return entry;
  }

  public async Task<DiaryEntry_Run?> CreateRunEntryAsync(DiaryEntry_Run_Create createRun)
  {
    // Mapping
    DiaryEntry_Run runEntry = _mapper.Map<DiaryEntry_Run>(createRun);

    // SQL Context
    ICollection<DiaryEntry_Run>? runEntries = _sqlContext.Diaries
      .Include(d => d.RunEntries)
      .First(d => d.OwnerId == createRun.DiaryId)
      .RunEntries;

    runEntry.EntryNr = runEntries.Last().EntryNr + 1;

    runEntries.Add(runEntry);

    // Warning: Untested
    await _mongoService.SaveToMongoDBAsync("Runs", runEntry.Run);

    await _sqlContext.SaveChangesAsync();

    // Return
    return runEntry;
  }
  #endregion Create

  #region Read
  // Notice: Not awaiting anything
  public async Task<IEnumerable<DiaryEntry_Food>?> ReadFoodEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    // SQL Context
    IEnumerable<DiaryEntry_Food> foods = _sqlContext.Diaries
      .Include(d => d.FoodEntries).ThenInclude(fe => fe.Food)
      .First(d => d.OwnerId == diaryId)
      .FoodEntries
      .Where(f => f.Acted >= start && f.Acted <= end)
      .AsEnumerable();
    // Return
    return foods;
  }

  public async Task<IEnumerable<DiaryEntry_Workout>?> ReadWorkoutEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    // SQL Context
    IEnumerable<DiaryEntry_Workout> workouts = _sqlContext.Diaries
      .Include(d => d.WorkoutEntries).ThenInclude(we => we.Workout)
      .First(d => d.OwnerId == diaryId)
      .WorkoutEntries
      .Where(w => w.Acted >= start && w.Acted <= end)
      .AsEnumerable();
    // Return
    return workouts;
  }

  public async Task<IEnumerable<DiaryEntry_BodyMetric>?> ReadBodyMetricEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    // SQL Context
    IEnumerable<DiaryEntry_BodyMetric> entries = _sqlContext.Diaries
      .Include(d => d.BodyMetrics)
      .First(d => d.OwnerId == diaryId)
      .BodyMetrics
      .Where(w => w.Acted >= start && w.Acted <= end)
      .AsEnumerable();
    // Return
    return entries;
  }

  public async Task<IEnumerable<DiaryEntry_Run>?> ReadRunEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    // SQL Context
    IEnumerable<DiaryEntry_Run> runEntries = _sqlContext.Diaries
      .Include(d => d.FoodEntries)
      .First(d => d.OwnerId == diaryId)
      .RunEntries
      .Where(r => r.Acted >= start && r.Acted <= end)
      .AsEnumerable();

    // MongoDB Helper
    foreach (DiaryEntry_Run runEntry in runEntries)
    {
      runEntry.Run = await _mongoService.GetFromMongoDBAsync<Run>("Runs", runEntry.MongoDBId);
    }

    // Return
    return runEntries;
  }
  #endregion Read

  #region Update
  public async Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food_Update update)
  {
    DiaryEntry_Food updater = _mapper.Map<DiaryEntry_Food>(update);

    _sqlContext.DiaryFoods.Update(updater);
    await _sqlContext.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout_Update update)
  {
    DiaryEntry_Workout updater = _mapper.Map<DiaryEntry_Workout>(update);

    _sqlContext.DiaryWorkouts.Update(updater);
    await _sqlContext.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric_Update update)
  {
    DiaryEntry_BodyMetric updater = _mapper.Map<DiaryEntry_BodyMetric>(update);

    _sqlContext.BodyMetrics.Update(updater);
    await _sqlContext.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_Run?> UpdateRunEntryAsync(DiaryEntry_Run_Update update)
  {
    DiaryEntry_Run updater = _mapper.Map<DiaryEntry_Run>(update);

    _sqlContext.DiaryRuns.Update(updater);
    await _sqlContext.SaveChangesAsync();

    return updater;
  }
  #endregion Update

  #region Delete
  public async Task<bool> DeleteFoodEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_Food? delete = _sqlContext.Diaries
      .Include(d => d.FoodEntries)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .FoodEntries
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sqlContext.DiaryFoods.Remove(delete);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteWorkoutEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_Workout? delete = _sqlContext.Diaries
      .Include(d => d.WorkoutEntries)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .WorkoutEntries
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sqlContext.DiaryWorkouts.Remove(delete);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteBodyMetricEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_BodyMetric? delete = _sqlContext.Diaries
      .Include(d => d.BodyMetrics)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .BodyMetrics
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sqlContext.BodyMetrics.Remove(delete);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteRunEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_Run? delete = _sqlContext.Diaries
      .Include(d => d.RunEntries)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .RunEntries
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sqlContext.DiaryRuns.Remove(delete);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }
  #endregion Delete
}
