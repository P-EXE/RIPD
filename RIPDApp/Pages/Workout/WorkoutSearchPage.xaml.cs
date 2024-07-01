using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class WorkoutSearchPage : ContentPage
{
	private readonly WorkoutSearchVM _vm;
	public WorkoutSearchPage(WorkoutSearchVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}