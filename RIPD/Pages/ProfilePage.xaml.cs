using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class ProfilePage : ContentPage
{
	private ProfilePageVM _vm;
	public ProfilePage(ProfilePageVM vm)
	{
		InitializeComponent();
		_vm = vm;
	}
}