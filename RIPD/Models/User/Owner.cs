namespace RIPD.Models;

public class Owner : User
{
  public Owner() { }
  public Owner(
    string name,
    string displayName,
    string email,
    string password
    ) : base(
      name,
      displayName,
      email,
      password
    )
  { }
}
