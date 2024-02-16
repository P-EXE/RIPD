namespace RIPD.Views.StatusBar;

public partial class StatusBarView : ContentView
{
	public StatusBarView()
	{
		InitializeComponent();
		BindingContext = new StatusBarViewModel();
	}
}