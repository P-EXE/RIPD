namespace RIPDShared.Models;

public class Workout
{
  public Guid? Id { get; set; }
  public string? Name { get; set; }
  public Guid? ContributerId { get; set; }
  public AppUser? Contributer { get; set; }
  public DateTime? CreationDateTime { get; set; }
  public DateTime? UpdateDateTime { get; set; }
  public string? Description { get; set; }
  public string? Image { get; set; }
  public float? Energy { get; set; }
}
