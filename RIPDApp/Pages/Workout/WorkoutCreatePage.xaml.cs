using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class WorkoutCreatePage : ContentPage
{
	private readonly WorkoutDetailsVM _vm;
	public WorkoutCreatePage(WorkoutDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}