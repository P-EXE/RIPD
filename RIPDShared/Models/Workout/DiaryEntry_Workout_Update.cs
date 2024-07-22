namespace RIPDShared.Models;

public class DiaryEntry_Workout_Update : DiaryEntry_Update
{
  public Guid? WorkoutId { get; set; }
  public double Amount { get; set; }
}
