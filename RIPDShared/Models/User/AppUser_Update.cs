using Microsoft.AspNetCore.Identity;

namespace RIPDShared.Models;

public class AppUser_Update : IdentityUser<Guid>
{
  public ICollection<AppUser>? Following = new HashSet<AppUser>();
  public ICollection<AppUser>? Followers = new HashSet<AppUser>();
}
