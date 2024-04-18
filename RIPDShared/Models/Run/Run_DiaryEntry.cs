using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
[Owned]
public class Run_DiaryEntry : DiaryEntry
{
  public required string? MongoDBId { get; set; }
}
