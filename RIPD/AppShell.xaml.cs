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
      Routing.RegisterRoute(nameof(FoodDetailsPage), typeof(FoodDetailsPage));
      Routing.RegisterRoute(nameof(NewFoodPage), typeof(NewFoodPage));
      #endregion Routes
    }
  }
}
