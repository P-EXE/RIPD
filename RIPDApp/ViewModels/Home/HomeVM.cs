using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Pages.Run;

namespace RIPDApp.ViewModels;

public partial class HomeVM : ObservableObject
{
  [RelayCommand]
  async Task GoToDiary()
  {
    await Shell.Current.GoToAsync(nameof(DiaryTodayPage));
  }

  [RelayCommand]
  async Task GoToRun()
  {
    await Shell.Current.GoToAsync(nameof(RunPage));
  }
}
