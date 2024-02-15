using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RIPD.Models
{
  [PrimaryKey(nameof(Id))]
  public class DiaryFoodEntry
  {
    public required int Id { get; set; }
    public required int DiaryUserId { get; set; }
    public required Diary Diary { get; set; }
    public required int FoodId { get; set; }
    public required Food Food { get; set; }
    public required double Quantity { get; set; }
    public required DateTime ConsumptionDateTime { get; set; }
    public required DateTime EntryDateTime { get; set; }
  }
}
