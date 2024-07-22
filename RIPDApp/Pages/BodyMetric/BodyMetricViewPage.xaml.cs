using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class BodyMetricViewPage : ContentPage
{
	private readonly BodyMetricDetailsVM _vm;
	public BodyMetricViewPage(BodyMetricDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}