using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace RIPDShared.Models;

// DiaryEntry_Run that exists in a 1 to 1 relation to the Run.
// DiaryEntry_Run is one half of the loosely coupled relation.
// DiaryEntry_Run is stored in the SQL DB.
// DiaryEntry_Run holds the metadata to a Run.
// DiaryEntry_Run should not exist without a Run.
public class DiaryEntry_Run : DiaryEntry
{
  public ObjectId? MongoDBId { get; set; }
  public Run Run { get; set; }
}
