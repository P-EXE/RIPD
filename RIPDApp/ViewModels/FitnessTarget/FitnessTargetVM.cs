using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

public partial class FitnessTargetVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public FitnessTargetVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
    GetFitnessTarget();
  }

  [ObservableProperty]
  private DiaryEntry_FitnessTarget _fitnessTarget;

  private async Task GetFitnessTarget()
  {
    try
    {
      FitnessTarget = await _diaryService.GetFitnessTargetEntryAsync() ?? new();
    }
    catch (Exception ex)
    {
      await Shell.Current.DisplayAlert("Error", "Could not update.", "Return");
      return;
    }
  }

  [RelayCommand]
  private async Task Update()
  {
    DiaryEntry_FitnessTarget? fitnessTarget;
    fitnessTarget = await _diaryService.UpdateFitnessTargetEntryAsync(FitnessTarget);
    if (fitnessTarget == null)
    {
      await Shell.Current.DisplayAlert("Error", "Could not update.", "Return");
      return;
    }
    FitnessTarget = fitnessTarget;
  }
}
