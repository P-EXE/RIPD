using RIPDShared.Models;
using System.Threading.Tasks;

namespace RIPDApp.Services;

public interface IDiaryService
{
  #region Create / Add
  Task<DiaryEntry_Food?> AddFoodEntryAsync(DiaryEntry_Food entry);
  Task<DiaryEntry_Workout?> AddWorkoutEntryAsync(DiaryEntry_Workout entry);
  Task<DiaryEntry_BodyMetric?> AddBodyMetricEntryAsync(DiaryEntry_BodyMetric entry);
  Task<DiaryEntry_Run?> AddRunEntryAsync(DiaryEntry_Run_Create entry);
  // Fitness Target can only be Read or Updated
  #endregion Create / Add

  #region Read
  Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesAsync(Diary diary, DateTime startDate, DateTime endDate);
  Task<IEnumerable<DiaryEntry_Food>?> GetWorkoutEntriesAsync(Diary diary, DateTime startDate, DateTime endDate);
  Task<IEnumerable<DiaryEntry_BodyMetric>?> GetBodyMetricEntriesAsync(Diary diary, DateTime startDate, DateTime endDate);
  Task<DiaryEntry_FitnessTarget?> GetFitnessTargetEntryAsync();
  Task<IEnumerable<DiaryEntry_Run>?> GetRunEntriesAsync(DateTime startDate, DateTime endDate);
  #endregion Read

  #region Update
  Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food entry);
  Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout entry);
  Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric entry);
  Task<DiaryEntry_FitnessTarget?> UpdateFitnessTargetEntryAsync(DiaryEntry_FitnessTarget entry);
  #endregion Update

  #region Delete
  Task<bool> DeleteFoodEntryAsync(DiaryEntry_Food entry);
  Task<bool> DeleteWorkoutEntryAsync(DiaryEntry_Workout entry);
  Task<bool> DeleteBodyMetricEntryAsync(DiaryEntry_BodyMetric entry);
  // Fitness Target can only be Read or Updated
  Task<bool> DeleteRunEntryAsync(DiaryEntry_Run entry);
  #endregion Delete
}
