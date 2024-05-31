namespace RIPDShared.Models;

public class Workout_Create
{
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required Guid ContributerId { get; set; }
  public required float Energy { get; set; }
}
