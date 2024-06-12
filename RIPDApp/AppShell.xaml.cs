using RIPDApp.Pages;

namespace RIPDApp;

public partial class AppShell : Shell
{
  public AppShell()
  {
    InitializeComponent();

    #region Routes
    Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

    Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    
    Routing.RegisterRoute(nameof(DiaryTodayPage), typeof(DiaryTodayPage));
    Routing.RegisterRoute(nameof(DiaryWeekPage), typeof(DiaryWeekPage));
    Routing.RegisterRoute(nameof(DiaryMonthPage), typeof(DiaryMonthPage));

    Routing.RegisterRoute($"{nameof(FoodsPage)}/{nameof(FoodDetailsPage)}", typeof(FoodDetailsPage));
    Routing.RegisterRoute($"{nameof(FoodDetailsPage)}/{nameof(UserSearchPage)}", typeof(UserSearchPage));
    Routing.RegisterRoute($"{nameof(FoodDetailsPage)}/{nameof(BarcodeScannerPage)}", typeof(BarcodeScannerPage));

    Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
    Routing.RegisterRoute(nameof(OwnerProfilePage), typeof(OwnerProfilePage));

    Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    Routing.RegisterRoute(nameof(SettingsDevPage), typeof(SettingsDevPage));
    #endregion Routes
  }
}
