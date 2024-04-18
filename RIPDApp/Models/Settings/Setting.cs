namespace RIPDApp.Models.Settings;

internal class Setting
{
  public string Title { get; set; }
  public string Subtitle { get; set; }
}
internal class ToggleSetting : Setting
{
  public bool State { get; set; }
}
