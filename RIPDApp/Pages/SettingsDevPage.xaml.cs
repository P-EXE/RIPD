using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class SettingsDevPage : ContentPage
{
	public SettingsDevPage(SettingsDevVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}