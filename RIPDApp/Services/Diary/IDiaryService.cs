using RIPDShared.Models;

namespace RIPDApp.Services;

public interface IDiaryService
{
  Task<bool> AddFoodEntryToDiaryAsync(DiaryEntry_Food entry);
  Task<bool> AddWorkoutEntryToDiaryAsync(DiaryEntry_Workout entry);
  Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesInDateRange(Diary diary, DateTime startDate, DateTime endDate);
}
