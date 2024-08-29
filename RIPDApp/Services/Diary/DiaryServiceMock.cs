using RIPDShared.Models;

namespace RIPDApp.Services;

public class DiaryServiceMock : IDiaryService
{
  #region Create / Add
  public Task<DiaryEntry_Food?> AddFoodEntryAsync(DiaryEntry_Food entry)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_Workout?> AddWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    throw new NotImplementedException();
  }
  
  public Task<DiaryEntry_BodyMetric?> AddBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_Run?> AddRunEntryAsync(DiaryEntry_Run entry)
  {
    throw new NotImplementedException();
  }

  #endregion Create / Add

  #region Read
  public Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<DiaryEntry_Workout>?> GetWorkoutEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<DiaryEntry_BodyMetric>?> GetBodyMetricEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_FitnessTarget?> GetFitnessTargetEntryAsync()
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<DiaryEntry_Run>?> GetRunEntriesAsync(DateTime startDate, DateTime endDate)
  {
    throw new NotImplementedException();
  }
  #endregion Read

  #region Update
  public Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food entry)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_FitnessTarget?> UpdateFitnessTargetEntryAsync(DiaryEntry_FitnessTarget entry)
  {
    throw new NotImplementedException();
  }

  #endregion Update

  #region Delete
  public Task<bool> DeleteBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteFoodEntryAsync(DiaryEntry_Food entry)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteRunEntryAsync(DiaryEntry_Run entry)
  {
    throw new NotImplementedException();
  }
  #endregion Delete
}
