using RIPD.DataServices;
using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class OwnerProfilePage : ContentPage
{
	private readonly IUserDataService _userDataService;
	private OwnerProfileVM _vm;
	public OwnerProfilePage(IUserDataService userDataService, OwnerProfileVM vm)
	{
		_userDataService = userDataService;
		if (_userDataService.GetOwnerAsync().Result == null)
		{
			Shell.Current.Navigation.PushAsync(new RegisterPage(new RegisterVM(_userDataService)));
		}
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}