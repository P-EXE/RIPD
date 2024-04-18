using RIPDShared.Models.Imported;

namespace RIPDShared.Models;

public class Run_DiaryEntryDTO_Create : DiaryEntryDTO_Create
{
  public List<Location> Locations { get; set; }
}
