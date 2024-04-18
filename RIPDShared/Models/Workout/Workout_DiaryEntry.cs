using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
[Owned]
public class Workout_DiaryEntry : DiaryEntry
{
  public required int? WorkoutId { get; set; }
  public required Workout? Workout { get; set; }
  public required double? Amount { get; set; }
}
