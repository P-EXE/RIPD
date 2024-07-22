using System.ComponentModel.DataAnnotations;

namespace RIPDShared.Models;

public class DiaryEntry
{
  [Key]
  public Guid DiaryId { get; set; }
  public Diary? Diary { get; set; }
  [Key]
  public int EntryNr { get; set; }
  public DateTime Acted {  get; set; }
  public DateTime Added {  get; set; }
}
