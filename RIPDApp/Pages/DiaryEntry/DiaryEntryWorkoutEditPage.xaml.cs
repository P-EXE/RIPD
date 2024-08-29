using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryEntryWorkoutEditPage : ContentPage
{
	private readonly DiaryEntryVM _vm;
	public DiaryEntryWorkoutEditPage(DiaryEntryVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}