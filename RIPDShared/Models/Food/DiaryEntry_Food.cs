using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RIPDShared.Models;

[Owned]
[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
public class DiaryEntry_Food : DiaryEntry
{
  public required Guid? FoodId { get; set; }
  public required Food? Food { get; set; }
  public required double Amount { get; set; }
}
