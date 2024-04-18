using RIPDApp.ViewModels;

namespace RIPDApp.Views;

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