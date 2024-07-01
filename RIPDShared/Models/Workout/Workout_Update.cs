namespace RIPDShared.Models;

public class Workout_Update
{
  public string Name { get; set; }
  public string Description { get; set; }
  public Guid ContributerId { get; set; }
  public float Energy { get; set; }
}
