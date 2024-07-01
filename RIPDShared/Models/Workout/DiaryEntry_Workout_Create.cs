namespace RIPDShared.Models;

public class DiaryEntry_Workout_Create : DiaryEntry_Create
{
  public Guid? WorkoutId { get; set; }
  public double Amount { get; set; }
}
