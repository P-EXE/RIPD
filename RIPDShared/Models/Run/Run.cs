using MongoDB.Bson;
using RIPDShared.Models.Imported;

namespace RIPDShared.Models;

public class Run
{
  public ObjectId Id { get; set; }
  public List<Location> Locations { get; set; }
}