using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodSearchPage : ContentPage
{
	private FoodsVM _vm;
	public FoodSearchPage(FoodsVM vm)
	{ 
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
  }
}