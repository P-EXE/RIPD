using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryMonthPage : ContentPage
{
	private readonly DiaryVM _vm;
	public DiaryMonthPage(DiaryVM vm)
	{
		InitializeComponent();
		_vm = vm;
    BindingContext = _vm;
	}
}