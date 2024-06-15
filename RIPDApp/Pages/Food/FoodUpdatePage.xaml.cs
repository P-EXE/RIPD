using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodUpdatePage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodUpdatePage(FoodDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}