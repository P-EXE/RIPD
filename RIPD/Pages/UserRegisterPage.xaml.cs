using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class UserRegisterPage : ContentPage
{
	private readonly UserRegisterVM _vm;
	public UserRegisterPage(UserRegisterVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}