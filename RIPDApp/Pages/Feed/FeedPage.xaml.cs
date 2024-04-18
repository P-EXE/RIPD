using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FeedPage : ContentPage
{
	private readonly FeedVM _vm;
	public FeedPage(FeedVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}