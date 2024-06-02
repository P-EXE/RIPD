namespace RIPDShared.Models;

public class DiaryEntry_Food_Create : DiaryEntryDTO_Create
{
  public required Guid? FoodId { get; set; }
  public required double Amount { get; set; }
}
