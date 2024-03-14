using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class UserLoginPage : ContentPage
{
  private UserLoginVM _vm;
	public UserLoginPage(UserLoginVM vm)
	{
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }
}