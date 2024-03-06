using RIPD.ViewModels;

namespace RIPD.Views;

public partial class StatusBarV: ContentView
{
  public StatusBarV()
  {
    InitializeComponent();
    BindingContext = new StatusBarVM();
  }
  public StatusBarV(StatusBarVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}