using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodDetailsPage : ContentPage
{
	public FoodDetailsPage()
	{
		InitializeComponent();
		BindingContext = new FoodDetailsVM();
	}
}