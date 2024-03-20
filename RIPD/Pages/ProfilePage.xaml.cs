using RIPD.DataServices;
using RIPD.ViewModels;

namespace RIPD.Pages;

public partial class ProfilePage : ContentPage
{
  private ProfileVM _vm;
  public ProfilePage(ProfileVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }
}