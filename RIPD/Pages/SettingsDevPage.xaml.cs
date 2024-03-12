using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class SettingsDevPage : ContentPage
{
	public SettingsDevPage(SettingsDevVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}