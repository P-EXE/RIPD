using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodViewPage : ContentPage
{
	private readonly FoodDetailsVM _vm;
	public FoodViewPage(FoodDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}