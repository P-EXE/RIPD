using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RIPD.Models
{
  /// <summary>
  /// Represents a User with EF Core
  /// </summary>
  /// <remarks>
  /// Author: Paul
  /// </remarks>
  [PrimaryKey(nameof(Id))]
  public class User
  {
    public int Id { get; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime CreationDateTime { get; }
    public Diary Diary { get; set; }

    public User() { }
  }
}
