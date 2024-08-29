using Microsoft.Maui.Devices.Sensors;

namespace RIPDShared.Models;

public class DiaryEntry_Run_Create : DiaryEntry_Create
{
  public Run Run { get; set; } = new();
}
