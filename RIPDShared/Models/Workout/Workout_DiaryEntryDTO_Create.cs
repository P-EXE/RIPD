namespace RIPDShared.Models;

public class Workout_DiaryEntryDTO_Create : DiaryEntryDTO_Create
{
  public required int? WorkoutId { get; set; }
  public required double Amount { get; set; }
}
