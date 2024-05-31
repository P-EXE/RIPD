using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
[Owned]
public class DiaryEntry_Workout : DiaryEntry
{
  public required int? WorkoutId { get; set; }
  public required Workout? Workout { get; set; }
  public required double? Amount { get; set; }
}
