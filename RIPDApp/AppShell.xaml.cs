﻿using Microsoft.Maui.Controls;
using RIPDApp.Pages;

namespace RIPDApp
{
  public partial class AppShell : Shell
  {
    public AppShell()
    {
      InitializeComponent();

      #region Routes      
      Routing.RegisterRoute(nameof(FoodDetailsPage), typeof(FoodDetailsPage));
      Routing.RegisterRoute(nameof(NewFoodPage), typeof(NewFoodPage));

      Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
      Routing.RegisterRoute(nameof(OwnerProfilePage), typeof(OwnerProfilePage));
      Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
      Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

      Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
      Routing.RegisterRoute(nameof(SettingsDevPage), typeof(SettingsDevPage));
      #endregion Routes
    }
  }
}
