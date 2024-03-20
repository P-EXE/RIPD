namespace RIPD.Models;

public class DiaryFoodEntry_UpdateDTO
{
  public required int FoodId { get; set; }
  public required double Quantity { get; set; }
  public required DateTime ConsumptionDateTime { get; set; }
  public required DateTime EntryDateTime { get; set; }
}
