using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterLoginVM _vm;
	public RegisterPage(RegisterLoginVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}