using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class SettingsPage : ContentPage
{
  private SettingsVM _vm;
  public SettingsPage(SettingsVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }
}