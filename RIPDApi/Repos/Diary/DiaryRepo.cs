using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RIPDApi.Data;
using RIPDApi.Services;
using RIPDShared.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RIPDApi.Repos;

public class DiaryRepo : IDiaryRepo
{
  private readonly SQLDataBaseContext _sql;
  private readonly MongoDataBaseContext _mongo;
  private readonly IMapper _mapper;

  // Determines the ammount of Items returned via the Take method
  const int takeSize = 20;

  public DiaryRepo(SQLDataBaseContext sql, MongoDataBaseContext mongo, IMapper mapper)
  {
    _sql = sql;
    _mapper = mapper;
    _mongo = mongo;
  }

  #region Create
  public async Task<DiaryEntry_Food?> CreateFoodEntryAsync(DiaryEntry_Food_Create createFood)
  {
    // Mapping
    DiaryEntry_Food foodEntry = _mapper.Map<DiaryEntry_Food>(createFood);

    // SQL Context
    ICollection<DiaryEntry_Food> foodEntries = _sql.Diaries
      .Include(d => d.FoodEntries)
      .First(d => d.OwnerId == createFood.DiaryId)
      .FoodEntries;

    foodEntries.Add(foodEntry);

    await _sql.SaveChangesAsync();

    // Return
    return foodEntry;
  }

  public async Task<DiaryEntry_Workout?> CreateWorkoutEntryAsync(DiaryEntry_Workout_Create createWorkout)
  {
    // Mapping
    DiaryEntry_Workout workoutEntry = _mapper.Map<DiaryEntry_Workout>(createWorkout);

    // SQL Context
    ICollection<DiaryEntry_Workout> workoutEntries = _sql.Diaries
      .Include(d => d.WorkoutEntries)
      .First(d => d.OwnerId == createWorkout.DiaryId)
      .WorkoutEntries;

    workoutEntries.Add(workoutEntry);

    await _sql.SaveChangesAsync();

    // Return
    return workoutEntry;
  }

  public async Task<DiaryEntry_BodyMetric?> CreateBodyMetricEntryAsync(DiaryEntry_BodyMetric_Create create)
  {
    // Mapping
    DiaryEntry_BodyMetric entry = _mapper.Map<DiaryEntry_BodyMetric>(create);
    entry.EntryNr = 0;

    // SQL Context
    ICollection<DiaryEntry_BodyMetric> entries = _sql.Diaries
      .Include(d => d.BodyMetrics)
      .First(d => d.OwnerId == create.DiaryId)
      .BodyMetrics;

    entries.Add(entry);

    await _sql.SaveChangesAsync();

    // Return
    return entry;
  }

  public async Task<DiaryEntry_Run?> CreateRunEntryAsync(DiaryEntry_Run_Create create)
  {
    // Mapping
    DiaryEntry_Run entry = _mapper.Map<DiaryEntry_Run>(create);
    entry.Run.Id = ObjectId.GenerateNewId();
    entry.MongoDBId = entry.Run.Id;

    // Mongo Context
    await _mongo.Runs.AddAsync(entry.Run);

    // SQL Context
    ICollection<DiaryEntry_Run>? runEntries = _sql.Diaries
      .Include(d => d.RunEntries)
      .First(d => d.OwnerId == create.DiaryId)
      .RunEntries;

    runEntries.Add(entry);

    await _sql.SaveChangesAsync();
    await _mongo.SaveChangesAsync();

    // Return
    return entry;
  }
  #endregion Create

  #region Read
  // Notice: Not awaiting anything
  public async Task<IEnumerable<DiaryEntry_Food>?> ReadFoodEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    // SQL Context
    IEnumerable<DiaryEntry_Food> foods = _sql.Diaries
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
    IEnumerable<DiaryEntry_Workout> workouts = _sql.Diaries
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
    IEnumerable<DiaryEntry_BodyMetric> entries = _sql.Diaries
      .Include(d => d.BodyMetrics)
      .First(d => d.OwnerId == diaryId)
      .BodyMetrics
      .Where(w => w.Acted >= start && w.Acted <= end)
      .AsEnumerable();
    // Return
    return entries;
  }

  public Task<DiaryEntry_FitnessTarget?> ReadFitnessTargetEntryAsync(Guid diaryId)
  {
    DiaryEntry_FitnessTarget? entry;

    // SQL Context
    entry = _sql.Diaries
      .Include(d => d.FitnessTarget)
      .First(d => d.OwnerId == diaryId)
      .FitnessTarget;

    return Task.FromResult(entry);
  }

  public async Task<IEnumerable<DiaryEntry_Run>?> ReadRunEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    // SQL Context
    IEnumerable<DiaryEntry_Run> runEntries = _sql.Diaries
      .Include(d => d.FoodEntries)
      .First(d => d.OwnerId == diaryId)
      .RunEntries
      .Where(r => r.Acted >= start && r.Acted <= end)
      .AsEnumerable();

    // MongoDB Helper
    foreach (DiaryEntry_Run runEntry in runEntries)
    {
      runEntry.Run = await _mongo.Runs.FindAsync(runEntry.MongoDBId);
    }

    // Return
    return runEntries;
  }
  #endregion Read

  #region Update
  public async Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food_Update update)
  {
    DiaryEntry_Food updater = _mapper.Map<DiaryEntry_Food>(update);

    _sql.DiaryFoods.Update(updater);
    await _sql.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout_Update update)
  {
    DiaryEntry_Workout updater = _mapper.Map<DiaryEntry_Workout>(update);

    _sql.DiaryWorkouts.Update(updater);
    await _sql.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric_Update update)
  {
    DiaryEntry_BodyMetric updater = _mapper.Map<DiaryEntry_BodyMetric>(update);

    _sql.BodyMetrics.Update(updater);
    await _sql.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_FitnessTarget> UpdateFitnessTargetEntryAsync(DiaryEntry_FitnessTarget_Update update)
  {
    DiaryEntry_FitnessTarget updater = _mapper.Map<DiaryEntry_FitnessTarget>(update);

    _sql.FitnessTargets.Update(updater);
    await _sql.SaveChangesAsync();

    return updater;
  }

  public async Task<DiaryEntry_Run?> UpdateRunEntryAsync(DiaryEntry_Run_Update update)
  {
    DiaryEntry_Run updater = _mapper.Map<DiaryEntry_Run>(update);

    _sql.DiaryRuns.Update(updater);
    await _sql.SaveChangesAsync();

    return updater;
  }
  #endregion Update

  #region Delete
  public async Task<bool> DeleteFoodEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_Food? delete = _sql.Diaries
      .Include(d => d.FoodEntries)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .FoodEntries
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sql.DiaryFoods.Remove(delete);

    await _sql.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteWorkoutEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_Workout? delete = _sql.Diaries
      .Include(d => d.WorkoutEntries)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .WorkoutEntries
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sql.DiaryWorkouts.Remove(delete);

    await _sql.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteBodyMetricEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_BodyMetric? delete = _sql.Diaries
      .Include(d => d.BodyMetrics)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .BodyMetrics
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sql.BodyMetrics.Remove(delete);

    await _sql.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteRunEntryAsync(Guid diaryId, int deleteId)
  {
    // SQL Context
    DiaryEntry_Run? delete = _sql.Diaries
      .Include(d => d.RunEntries)
      .FirstOrDefault(d => d.OwnerId == diaryId)?
      .RunEntries
      .FirstOrDefault(bm => bm.EntryNr == deleteId);

    if (delete == null) return false;

    _sql.DiaryRuns.Remove(delete);

    await _sql.SaveChangesAsync();

    // Return
    return true;
  }
  #endregion Delete
}
