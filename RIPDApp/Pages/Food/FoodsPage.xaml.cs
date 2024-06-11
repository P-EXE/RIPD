using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FoodsPage : ContentPage
{
	private FoodsVM _vm;
	public FoodsPage(FoodsVM vm)
	{ 
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
  }
}