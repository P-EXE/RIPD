using RIPDApp.Services;

namespace RIPDApp.Pages;

public partial class AutoLoginPage : ContentPage
{
  private readonly IOwnerService _ownerService;
  public AutoLoginPage(IOwnerService ownerService)
  {
    _ownerService = ownerService;

    InitializeComponent();
  }

  protected override async void OnAppearing()
  {
    base.OnAppearing();

    await Task.Delay(1);

    bool success = await _ownerService.AutoLogin();
    if (success)
    {
      await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
    }
    else
    {
      await Shell.Current.GoToAsync($"///RegisterLogin");
    }
  }
}