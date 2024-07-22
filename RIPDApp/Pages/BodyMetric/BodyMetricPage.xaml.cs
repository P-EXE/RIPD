using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class BodyMetricPage : ContentPage
{
	private readonly BodyMetricVM _vm;
	public BodyMetricPage(BodyMetricVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}