using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RIPD.Models
{
  [PrimaryKey(nameof(Id))]
  public class User
  {
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? DisplayName { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime CreationDateTime { get; set; }
    public Diary Diary { get; set; }
  }
}
