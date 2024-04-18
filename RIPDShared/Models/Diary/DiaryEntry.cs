using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RIPDShared.Models;

[Owned]
[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
public class DiaryEntry
{
  [Key]
  public required Guid DiaryId { get; set; }
  public required Diary Diary { get; set; }
  [Key]
  public required int EntryNr { get; set; }
  public required DateTime Acted {  get; set; }
  public required DateTime Added {  get; set; }
}
