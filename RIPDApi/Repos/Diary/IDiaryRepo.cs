using RIPDShared.Models;

namespace RIPDApi.Repos;

// Every Model used here is in it's Entry form, unless specified otherwise.
// Every Method here is for the Entry form of the Model, unless specified otherwise.

// !!! Run is experimental !!!
public interface IDiaryRepo
{
  Task<DiaryEntry_Food?> CreateFoodEntryAsync(DiaryEntry_Food_Create createFood);
  Task<DiaryEntry_Workout?> CreateWorkoutEntryAsync(DiaryEntry_Workout_Create createWorkout);
  // Has to ensure the creation of the DiaryEntry_Run, as well as the Run itself.
  Task<DiaryEntry_Run?> CreateRunEntryAsync(DiaryEntry_Run_Create createRun);

  Task<IEnumerable<DiaryEntry_Food>?> ReadFoodEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end);
  Task<IEnumerable<DiaryEntry_Workout>?> ReadWorkoutEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end);
  // Has to ensure that both the DiaryEntry_Run aswell as the Run itself are read and returned.
  Task<IEnumerable<DiaryEntry_Run>?> ReadRunEntriesFromToDateAsync(Guid diaryId, DateTime start, DateTime end);

  Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food_Update updateFood);
  Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout_Update updateWorkout);
  // Since a Run can hardly be updated this should only concern metadata in DiaryEntry_Run.
  Task<DiaryEntry_Run?> UpdateRunEntryAsync(DiaryEntry_Run_Update updateRun);

  Task<bool> DeleteFoodEntryAsync(Guid diaryId, int deleteFoodId);
  Task<bool> DeleteWorkoutEntryAsync(Guid diaryId, int deleteWorkoutId);
  // Has to ensure the deletion of the DiaryEntry_Run, as well as the Run itself.
  Task<bool> DeleteRunEntryAsync(Guid diaryId, int deleteRunId);
}
