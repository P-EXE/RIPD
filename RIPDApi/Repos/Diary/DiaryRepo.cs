using AutoMapper;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Repos;

public class DiaryRepo : IDiaryRepo
{
  private readonly SQLDataBaseContext _sqlContext;
  private readonly IMapper _mapper;

  // Determines the ammount of Items returned via the Take method
  const int takeSize = 20;

  public DiaryRepo(SQLDataBaseContext sqlContext, IMapper mapper)
  {
    _sqlContext = sqlContext;
    _mapper = mapper;
  }

  #region Create
  public async Task<DiaryEntry_Food?> CreateFoodEntryAsync(DiaryEntry_Food_Create createFood)
  {
    throw new NotImplementedException();
  }
  public async Task<DiaryEntry_Workout?> CreateWorkoutEntryAsync(DiaryEntry_Workout_Create createWorkout)
  {
    throw new NotImplementedException();
  }
  public async Task<DiaryEntry_Run?> CreateRunEntryAsync(DiaryEntry_Run_Create createRun)
  {
    throw new NotImplementedException();
  }
  #endregion Create

  #region Read
  public async Task<IEnumerable<DiaryEntry_Food>?> ReadFoodEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    throw new NotImplementedException();
  }
  public async Task<IEnumerable<DiaryEntry_Workout>?> ReadWorkoutEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    throw new NotImplementedException();
  }
  public async Task<IEnumerable<DiaryEntry_Run>?> ReadRunEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end)
  {
    throw new NotImplementedException();
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
    throw new NotImplementedException();
  }
  public async Task<bool> DeleteWorkoutEntryAsync(Guid diaryId, int deleteWorkoutId)
  {
    throw new NotImplementedException();
  }
  public async Task<bool> DeleteRunEntryAsync(Guid diaryId, int deleteRunId)
  {
    throw new NotImplementedException();
  }
  #endregion Delete
}
