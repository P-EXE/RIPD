using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryEntryWorkoutCreatePage : ContentPage
{
	private readonly DiaryEntryVM _vm;
	public DiaryEntryWorkoutCreatePage(DiaryEntryVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}