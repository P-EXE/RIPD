using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class LoginPage : ContentPage
{
	private LoginVM _vm;
	public LoginPage(LoginVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}