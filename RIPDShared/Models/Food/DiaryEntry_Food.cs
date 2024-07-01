namespace RIPDShared.Models;

public class DiaryEntry_Food : DiaryEntry
{
  public Guid? FoodId { get; set; }
  public Food? Food { get; set; }
  public double Amount { get; set; }
}
