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

  protected override async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    base.OnNavigatedTo(args);

    bool success = await _ownerService.AutoLogin();
    if (success)
    {
      await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
    }
    else
    {
      await Shell.Current.GoToAsync($"///{nameof(RegisterPage)}");
    }
  }
}