using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodSearchPage : ContentPage
{
	private FoodSearchVM _vm;
	public FoodSearchPage(FoodSearchVM vm)
	{ 
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
  }
}