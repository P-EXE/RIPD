using Microsoft.Maui.Controls;
using RIPD.Pages;

namespace RIPD
{
  public partial class AppShell : Shell
  {
    public AppShell()
    {
      InitializeComponent();

      #region Routes
      Routing.RegisterRoute(nameof(UserRegisterPage), typeof(UserRegisterPage));
      Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
      
      Routing.RegisterRoute(nameof(FoodDetailsPage), typeof(FoodDetailsPage));
      Routing.RegisterRoute(nameof(NewFoodPage), typeof(NewFoodPage));

      Routing.RegisterRoute(nameof(SettingsDevPage), typeof(SettingsDevPage));
      #endregion Routes
    }
  }
}
