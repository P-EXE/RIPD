using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodCreatePage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodCreatePage(FoodDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}