using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RIPDShared.Models;

[Owned]
[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
public class DiaryEntry_Food : DiaryEntry
{
  public Guid? FoodId { get; set; }
  public Food? Food { get; set; }
  public double Amount { get; set; }
}
