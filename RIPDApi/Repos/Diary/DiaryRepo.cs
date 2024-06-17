using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDApi.Services;
using RIPDShared.Models;

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
    ICollection<DiaryEntry_Food>? foodEntries = _sqlContext.Diaries
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
    ICollection<DiaryEntry_Workout>? workoutEntries = _sqlContext.Diaries
      .Include(d => d.WorkoutEntries)
      .First(d => d.OwnerId == createWorkout.DiaryId)
      .WorkoutEntries;

    workoutEntries.Add(workoutEntry);

    await _sqlContext.SaveChangesAsync();

    // Return
    return workoutEntry;
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
      .Include(d => d.FoodEntries)
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
      .Include(d => d.FoodEntries)
      .First(d => d.OwnerId == diaryId)
      .WorkoutEntries
      .Where(w => w.Acted >= start && w.Acted <= end)
      .AsEnumerable();
    // Return
    return workouts;
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
  public async Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food_Update updateFood)
  {
    throw new NotImplementedException();
  }

  public async Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout_Update updateWorkout)
  {
    throw new NotImplementedException();
  }

  public async Task<DiaryEntry_Run?> UpdateRunEntryAsync(DiaryEntry_Run_Update updateRun)
  {
    throw new NotImplementedException();
  }
  #endregion Update

  #region Delete
  public async Task<bool> DeleteFoodEntryAsync(Guid diaryId, int deleteFoodId)
  {
    // SQL Context
    DiaryEntry_Food? deleteFood = await _sqlContext.DiaryFoods.FindAsync($"{diaryId}{deleteFoodId}");
    if (deleteFood == null) return false;

    _sqlContext.DiaryFoods.Remove(deleteFood);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteWorkoutEntryAsync(Guid diaryId, int deleteWorkoutId)
  {
    // SQL Context
    DiaryEntry_Workout? deleteWorkout = await _sqlContext.DiaryWorkouts.FindAsync($"{diaryId}{deleteWorkoutId}");
    if (deleteWorkout == null) return false;

    _sqlContext.DiaryWorkouts.Remove(deleteWorkout);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }

  public async Task<bool> DeleteRunEntryAsync(Guid diaryId, int deleteRunId)
  {
    // SQL Context
    DiaryEntry_Run? deleteRun = await _sqlContext.DiaryRuns.FindAsync($"{diaryId}{deleteRunId}");
    if (deleteRun == null) return false;

    await _mongoService.DeleteFromMongoDBAsync<Run>("Runs", deleteRun.MongoDBId);

    _sqlContext.DiaryRuns.Remove(deleteRun);

    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }
  #endregion Delete
}
