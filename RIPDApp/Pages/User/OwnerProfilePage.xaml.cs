using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class OwnerProfilePage : ContentPage
{
	private OwnerProfileVM _vm;
	public OwnerProfilePage(OwnerProfileVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}