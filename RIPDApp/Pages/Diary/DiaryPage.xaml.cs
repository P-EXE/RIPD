using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryPage : ContentPage
{
	private readonly DiaryVM _vm;
	public DiaryPage(DiaryVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}