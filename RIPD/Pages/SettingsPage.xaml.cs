namespace RIPD.Pages;

public partial class SettingsPage : ContentPage
{
  public SettingsPage()
  {
    InitializeComponent();
  }

  private async void SettingsDev_Clicked(object sender, EventArgs e)
  {
    await Shell.Current.GoToAsync($"{nameof(SettingsDevPage)}", true);
  }
}