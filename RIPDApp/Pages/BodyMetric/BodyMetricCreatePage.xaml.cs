using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class BodyMetricCreatePage : ContentPage
{
	private readonly BodyMetricDetailsVM _vm;
	public BodyMetricCreatePage(BodyMetricDetailsVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}