using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class UserProfilePage : ContentPage
{
  private UserProfileVM _vm;
  public UserProfilePage(UserProfileVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }
}