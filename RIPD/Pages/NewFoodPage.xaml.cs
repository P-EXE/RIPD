using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class NewFoodPage : ContentPage
{
	public NewFoodPage(NewFoodVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}