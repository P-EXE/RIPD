using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class UserProfileUpdatePage : ContentPage
{
  private UserProfileVM _vm;
  public UserProfileUpdatePage(UserProfileVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }
}