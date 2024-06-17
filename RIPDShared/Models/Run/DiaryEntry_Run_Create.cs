using RIPDShared.Models.Imported;

namespace RIPDShared.Models;

public class DiaryEntry_Run_Create : DiaryEntry_Create
{
  public List<Location> Locations { get; set; }
}
