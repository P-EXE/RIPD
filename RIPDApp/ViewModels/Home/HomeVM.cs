using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;

namespace RIPDApp.ViewModels;

public partial class HomeVM : ObservableObject
{
  [RelayCommand]
  async Task GoToDiary()
  {
    await Shell.Current.GoToAsync(nameof(DiaryTodayPage));
  }
}
