using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodDetailsViewPage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodDetailsViewPage(FoodDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}