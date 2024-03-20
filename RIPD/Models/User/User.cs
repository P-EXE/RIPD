using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RIPD.Models;

/// <summary>
/// Represents a User with EF Core
/// </summary>
/// <remarks>
/// Author: Paul
/// </remarks>
[PrimaryKey(nameof(Id))]
[Index(nameof(Email), IsUnique = true)]
//[Index(nameof(Token), IsUnique = true)]
public class User
{
  private int _id;
  private string? _token;
  private string _name;
  private string _displayName;
  private string _email;
  private string _password;
  private DateTime _created;
  private Diary _diary = new();
  private ICollection<User?> _following = new HashSet<User?>();
  private ICollection<User?> _followers = new HashSet<User?>();

  [Required]
  public int Id { get => _id; }
  [Required]
  public string Token { get => _token; }
  [Required]
  public string Name { get => _name; set => _name = value; }
  [Required]
  public string DisplayName { get => _displayName; set => _displayName = value; }
  [Required]
  public string Email { get => _email; set => _email = value; }
  [Required]
  public string Password { get => _password; set => _password = value; }
  [Required]
  public DateTime Created { get => _created; }
  [Required]
  public Diary Diary { get => _diary; set => _diary = value; }
  [Required]
  public ICollection<User> Following { get => _following; set => _following = value; }
  [Required]
  public ICollection<User> Followers { get => _followers; set => _followers = value; }

  public User()
  {
  }

  public User(
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
