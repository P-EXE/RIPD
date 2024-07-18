using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

[Owned]
[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
public class BodyMetric : DiaryEntry
{
  public double? Height { get; set; }
  public double? Weight { get; set; }
}