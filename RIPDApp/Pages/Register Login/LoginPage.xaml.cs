using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class LoginPage : ContentPage
{
	private RegisterLoginVM _vm;
	public LoginPage(RegisterLoginVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}