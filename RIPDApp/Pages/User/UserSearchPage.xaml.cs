using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class UserSearchPage : ContentPage
{
	private readonly UserSearchVM _vm;
	public UserSearchPage(UserSearchVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}