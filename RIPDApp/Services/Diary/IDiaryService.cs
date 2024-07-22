using RIPDShared.Models;

namespace RIPDApp.Services;

public interface IDiaryService
{
  Task<bool> AddFoodEntryyAsync(DiaryEntry_Food entry);
  Task<bool> AddWorkoutEntryAsync(DiaryEntry_Workout entry);
  Task<DiaryEntry_BodyMetric?> AddBodyMetricEntryAsync(DiaryEntry_BodyMetric entry);
  Task<IEnumerable<DiaryEntry_BodyMetric>?> GetBodyMetricEntriesAsync(Diary diary, DateTime startDate, DateTime endDate);
  Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric entry);
  Task<bool> DeleteBodyMetricEntryAsync(DiaryEntry_BodyMetric entry);
  Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesAsync(Diary diary, DateTime startDate, DateTime endDate);
}
