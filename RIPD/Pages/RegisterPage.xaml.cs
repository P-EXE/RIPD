using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterVM _vm;
	public RegisterPage(RegisterVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}