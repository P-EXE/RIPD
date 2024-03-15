﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class UserRegisterVM : ObservableObject
  {
    private readonly UserDataServiceAPI _userDataService;
    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private string? _displayName;
    [ObservableProperty]
    private string? _email;
    [ObservableProperty]
    private string? _password;

    public UserRegisterVM(UserDataServiceAPI userDataService)
    {
      _userDataService = userDataService;
    }

    /// <summary>
    /// Tries creating a User model and checks if it already exists.
    /// If it does not, tell the UserDataService to create it.
    /// If it does, navigate to the LoginPage
    /// </summary>
    /// <returns>Nothing for now</returns>
    [RelayCommand]
    private async Task Register()
    {
      User user = new();
      try
      {
        user = new()
        {
          Name = Name,
          DisplayName = DisplayName,
          Email = Email,
          Password = Password
        };

        user = await _userDataService.GetOneAsync(("email", Email));

        if (user == null)
        {
          await _userDataService.CreateAsync(user);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("--> Custom(Error): UserRegisterVM.Register: Format incorrect!");
      }
      return;
    }
  }
}
