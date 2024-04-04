using System.ComponentModel.DataAnnotations;

namespace RIPD.Models;

public class User_CreateDTO
{
  private string _name;
  private string _displayName;
  private string _email;
  private string _password;

  [Required]
  public string Name { get => _name; set => _name = value; }
  [Required]
  public string DisplayName { get => _displayName; set => _displayName = value; }
  [Required]
  public string Email { get => _email; set => _email = value; }
  [Required]
  public string Password { get => _password; set => _password = value; }

  public User_CreateDTO()
  {
  }

  public User_CreateDTO(
  string name,
  string displayName,
  string email,
  string password
  )
  {
    Name = name;
    DisplayName = displayName;
    Email = email;
    Password = password;
  }
}
