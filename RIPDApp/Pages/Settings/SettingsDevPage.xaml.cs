using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class SettingsDevPage : ContentPage
{
	private readonly SettingsDevVM _vm;
	public SettingsDevPage(SettingsDevVM vm)
	{
    _vm = vm;
    BindingContext = _vm;
    InitializeComponent();
	}
}