using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using RIPD.DataServices;
using RIPD.Models.ApiConnection;

namespace RIPD.ViewModels
{
  public partial class SettingsDevVM : ObservableObject
  {
    private LocalDBContext _localDBContext;

    [ObservableProperty]
    private List<ApiConnection> _apiConnections;

    [ObservableProperty]
    private ApiConnection _apiConnection;

    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _protocol;
    [ObservableProperty]
    private string _domain;
    [ObservableProperty]
    private int _port;
    [ObservableProperty]
    private string _apiPage;
    [ObservableProperty]
    private string _statusPage;

    public SettingsDevVM(LocalDBContext localDBContext)
    {
      _localDBContext = localDBContext;
      _apiConnections = [.. _localDBContext.ApiConnections];
    }

    [RelayCommand]
    private async Task UseApiConnection()
    {
      _localDBContext.ApiConnections.ExecuteUpdate(e => e.SetProperty(p => p.Active, false));
      ApiConnection.Active = true;
      _localDBContext.Update(ApiConnection);
    }

    [RelayCommand]
    private async Task AddApiConnection()
    {
      ApiConnection apiConnection = new()
      {
        Name = Name,
        Protocol = Protocol,
        Domain = Domain,
        Port = Port,
        ApiPage = ApiPage,
        StatusPage = StatusPage,
        Active = false,
      };
      await _localDBContext.AddAsync(apiConnection);
      await _localDBContext.SaveChangesAsync();
      return;
    }

    [RelayCommand]
    private async Task RemoveApiConnection()
    {
      _localDBContext.Remove(ApiConnection);
      _localDBContext.SaveChanges();
    }
  }
}
