using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryTodayPage : ContentPage
{
	private readonly DiaryVM _vm;
	public DiaryTodayPage(DiaryVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}