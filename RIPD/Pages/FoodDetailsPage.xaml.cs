using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class FoodDetailsPage : ContentPage
{
	public FoodDetailsPage()
	{
		InitializeComponent();
		BindingContext = new FoodDetailsVM();
	}
}