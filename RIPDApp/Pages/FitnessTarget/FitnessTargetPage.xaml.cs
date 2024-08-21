using RIPDApp.ViewModels;

namespace RIPDApp.Pages;

public partial class FitnessTargetPage : ContentPage
{
	public FitnessTargetPage(FitnessTargetVM vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}