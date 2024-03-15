using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class NewFoodPage : ContentPage
{
	private readonly NewFoodVM _vm;
	public NewFoodPage(NewFoodVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}