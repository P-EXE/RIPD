using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodDetailsPage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodDetailsPage(FoodDetailsVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}