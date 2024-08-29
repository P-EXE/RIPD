using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryEntryFoodEditPage : ContentPage
{
	private readonly DiaryEntryVM _vm;	
	public DiaryEntryFoodEditPage(DiaryEntryVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}