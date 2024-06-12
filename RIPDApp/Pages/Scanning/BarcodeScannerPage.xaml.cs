using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class BarcodeScannerPage : ContentPage
{
	private readonly ScannerVM _vm;
	public BarcodeScannerPage(ScannerVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}