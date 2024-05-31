using RIPDShared.Models.Imported;

namespace RIPDShared.Models;

public class DiaryEntry_Run_Create : DiaryEntryDTO_Create
{
  public List<Location> Locations { get; set; }
}
