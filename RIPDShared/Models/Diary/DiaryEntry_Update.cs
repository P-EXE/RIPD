namespace RIPDShared.Models;

public class DiaryEntry_Update
{
  public Guid DiaryId { get; set; }
  public int EntryNr { get; set; }
  public DateTime Acted { get; set; }
}
