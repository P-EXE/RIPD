using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class ProfilePage : ContentPage
{
  private ProfileVM _vm;
  public ProfilePage(ProfileVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }
}