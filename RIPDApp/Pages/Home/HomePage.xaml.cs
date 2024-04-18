using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class HomePage : ContentPage
{
	private readonly HomeVM _vm;
	public HomePage(HomeVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}