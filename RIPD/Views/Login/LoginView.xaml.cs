namespace RIPD.Views.Login;

public partial class LoginView : ContentView
{
	public LoginView()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
}