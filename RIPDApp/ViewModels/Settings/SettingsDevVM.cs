using CommunityToolkit.Mvvm.ComponentModel;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

public partial class SettingsDevVM : ObservableObject
{
  private readonly IOwnerService _ownerService;

  public SettingsDevVM(IOwnerService ownerService)
  {
    _ownerService = ownerService;
    BearerToken = Statics.Auth.BearerToken;
  }

  [ObservableProperty]
  private BearerToken? _bearerToken;
}
