using Microsoft.AspNetCore.Identity;

namespace RIPDShared.Models;

public class AppUser : IdentityUser<Guid>
{
  public ICollection<Food>? ContributedFoods = new HashSet<Food>();
  public ICollection<Food>? ManufacturedFoods = new HashSet<Food>();
  public ICollection<Workout>? ContributedWorkouts = new HashSet<Workout>();
  public ICollection<AppUser>? Following = new HashSet<AppUser>();
  public ICollection<AppUser>? Followers = new HashSet<AppUser>();
  public Guid DiaryId { get; set; }
  public Diary? Diary {  get; set; } = new();
}
