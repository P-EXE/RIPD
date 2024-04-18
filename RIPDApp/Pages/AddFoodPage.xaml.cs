using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class AddFoodPage : ContentPage
{
	private AddFoodVM _vm;
	public AddFoodPage(AddFoodVM vm)
	{ 
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
  }
}