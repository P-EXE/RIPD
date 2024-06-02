using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

// DiaryEntry_Run that exists in a 1 to 1 relation to the Run.
// DiaryEntry_Run is one half of the loosely coupled relation.
// DiaryEntry_Run is stored in the SQL DB.
// DiaryEntry_Run holds the metadata to a Run.
// DiaryEntry_Run should not exist without a Run.
[PrimaryKey(nameof(DiaryId), nameof(EntryNr))]
[Owned]
public class DiaryEntry_Run : DiaryEntry
{
  public string? MongoDBId { get; set; }
  // This should normally be null.
  // Can be filled at runtime to allow easier CRUD operations.
  // ↑ Previous statement needs to be tested.
/*  public Run? Run { get; set; }*/
}
