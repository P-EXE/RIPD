namespace RIPDShared.Models;

public class DiaryEntry_Workout_Create : DiaryEntry_Create
{
  public required int? WorkoutId { get; set; }
  public required double Amount { get; set; }
}
