using RIPDApp.Pages;
using RIPDApp.Services;

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

    #region FoodSearch
    // Food Search -> Barcode Scanner
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(BarcodeScannerPage)}",
      typeof(BarcodeScannerPage)
      );
    // Food Search -> Food Diary Entry
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(DiaryEntryFoodCreatePage)}",
      typeof(DiaryEntryFoodCreatePage)
      );
    // Food Search -> Food View
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(FoodViewPage)}",
      typeof(FoodViewPage)
      );
    #endregion FoodSearch

    #region FoodDetails
    // Food Details -> Food Update
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(FoodViewPage)}" +
      $"/{nameof(FoodUpdatePage)}",
      typeof(FoodUpdatePage)
      );
    // Food Search -> Food Create
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(FoodCreatePage)}",
      typeof(FoodCreatePage)
      );

    // Food Create -> User Search
    Routing.RegisterRoute(
      $"{nameof(FoodCreatePage)}" +
      $"/{nameof(UserSearchPage)}",
      typeof(UserSearchPage)
      );
    // Food Create -> Scan Barcode
    Routing.RegisterRoute(
      $"{nameof(FoodCreatePage)}" +
      $"/{nameof(BarcodeScannerPage)}",
      typeof(BarcodeScannerPage)
      );
    #endregion FoodDetails

    Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
    Routing.RegisterRoute(nameof(OwnerProfilePage), typeof(OwnerProfilePage));

    Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    Routing.RegisterRoute(nameof(SettingsDevPage), typeof(SettingsDevPage));
    #endregion Routes
  }
}
