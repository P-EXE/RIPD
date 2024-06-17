namespace RIPDShared.Models;

public class DiaryEntry_Food_Create : DiaryEntry_Create
{
  public Guid? FoodId { get; set; }
  public double Amount { get; set; }
}
