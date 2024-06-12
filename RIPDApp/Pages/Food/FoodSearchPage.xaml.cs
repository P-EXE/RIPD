using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodSearchPage : ContentPage
{
	private FoodListVM _vm;
	public FoodSearchPage(FoodListVM vm)
	{ 
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
  }
}