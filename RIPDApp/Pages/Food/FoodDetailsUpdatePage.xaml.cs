using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodDetailsUpdatePage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodDetailsUpdatePage(FoodDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}