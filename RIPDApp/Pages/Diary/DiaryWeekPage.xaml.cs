using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class DiaryWeekPage : ContentPage
{
	private readonly DiaryVM _vm;
	public DiaryWeekPage(DiaryVM vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}