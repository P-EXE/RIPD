using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class RunPage : ContentPage
{
	private readonly RunVM _vm;
	public RunPage(RunVM vm)
	{
		InitializeComponent();

		_vm = vm;
		BindingContext = _vm;
	}
}