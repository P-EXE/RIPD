using RIPD.DataServices;
using RIPD.ViewModels;

namespace RIPD.Views;

public partial class StatusBarV : ContentView
{
  public StatusBarV()
  {
    InitializeComponent();
  }
  public StatusBarV(StatusBarVM vm)
  {
    InitializeComponent();
    BindingContext = vm;
  }
}