namespace RIPD.Models;

// Id will be auto generated
public class DiaryFoodEntry_CreateDTO
{
  public required int DiaryUserId { get; set; }
  public required int FoodId { get; set; }
  public required double Quantity { get; set; }
  public required DateTime ConsumptionDateTime { get; set; }
  public required DateTime EntryDateTime { get; set; }
}
