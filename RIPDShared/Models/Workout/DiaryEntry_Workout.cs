namespace RIPDShared.Models;

public class DiaryEntry_Workout : DiaryEntry
{
  public Guid? WorkoutId { get; set; }
  public Workout? Workout { get; set; }
  public double Amount { get; set; }
}
