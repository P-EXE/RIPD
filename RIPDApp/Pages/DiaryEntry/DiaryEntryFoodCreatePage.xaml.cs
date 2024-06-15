using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryEntryFoodCreatePage : ContentPage
{
	private readonly DiaryEntryVM _vm;	
	public DiaryEntryFoodCreatePage(DiaryEntryVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}