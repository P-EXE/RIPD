namespace RIPDShared.Models;

public class Food_DiaryEntryDTO_Create : DiaryEntryDTO_Create
{
  public required int? FoodId { get; set; }
  public required double Amount { get; set; }
}
