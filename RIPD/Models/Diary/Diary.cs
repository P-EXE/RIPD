using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RIPD.Models
{
  public class Diary
  {
    [Required]
    [Key]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; }
    public List<DiaryFoodEntry> FoodEntries { get; set; }
  }
}
