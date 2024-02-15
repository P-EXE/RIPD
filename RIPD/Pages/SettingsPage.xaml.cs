namespace RIPD.Pages;

public partial class SettingsPage : ContentPage
{
  public SettingsPage()
  {
    InitializeComponent();
  }

  private async void SettingsDev_Clicked(object sender, EventArgs e)
  {
    await Navigation.PushAsync(new SettingsDevPage(),true);
  }
}