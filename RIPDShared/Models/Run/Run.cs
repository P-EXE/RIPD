using MongoDB.Bson;
using RIPDShared.Models.Imported;

namespace RIPDShared.Models;

// Run that exists in a 1 to 1 relation to the DiaryEntry_Run.
// Run is one half of the loosely coupled relation.
// Run is stored in MongoDB.
// Run holds the actual telemetry to a DiaryEntry_Run.
// Run should not exist without a DiaryEntry_Run.
public class Run
{
  public ObjectId Id { get; set; }
  public List<Location> Locations { get; set; }
}