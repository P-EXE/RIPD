﻿using RIPDApp.Pages;

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

    // Food Related Routes
    // Food Search -> Food Details
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(FoodDetailsViewPage)}",
      typeof(FoodDetailsViewPage)
      );
    // Food Details -> Food Update
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(FoodDetailsViewPage)}" +
      $"/{nameof(FoodDetailsUpdatePage)}",
      typeof(FoodDetailsUpdatePage)
      );
    // Food Search -> Food Create
    Routing.RegisterRoute(
      $"{nameof(FoodSearchPage)}" +
      $"/{nameof(FoodDetailsCreatePage)}", 
      typeof(FoodDetailsCreatePage)
      );

    Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
    Routing.RegisterRoute(nameof(OwnerProfilePage), typeof(OwnerProfilePage));

    Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    Routing.RegisterRoute(nameof(SettingsDevPage), typeof(SettingsDevPage));
    #endregion Routes
  }
}
