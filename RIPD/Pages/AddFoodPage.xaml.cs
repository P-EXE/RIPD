using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class AddFoodPage : ContentPage
{
	public AddFoodPage(AddFoodVM vm)
	{ 
		InitializeComponent();
		BindingContext = vm;
  }
}