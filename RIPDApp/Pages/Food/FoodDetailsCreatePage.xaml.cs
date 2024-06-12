using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodDetailsCreatePage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodDetailsCreatePage(FoodDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}