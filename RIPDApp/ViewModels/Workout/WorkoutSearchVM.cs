using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Compatibility;
using RIPDApp.Messaging;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

public partial class WorkoutSearchVM : ObservableObject
{
  private readonly IWorkoutService _workoutService;

  public WorkoutSearchVM(IWorkoutService workoutService)
  {
    _workoutService = workoutService;
    WeakReferenceMessenger.Default.Register<PageReturnObjectMessage<string>>(this, (r, m) =>
    {
      SearchText = m.Value;
    });
  }

  [ObservableProperty]
  private bool _isRefreshing;
  [ObservableProperty]
  private string _searchText;
  [ObservableProperty]
  private ObservableCollection<Workout>? _workouts;
  [ObservableProperty]
  private Workout? _selectedWorkout;

  [RelayCommand]
  async Task Search()
  {
    IEnumerable<Workout>? workouts = await _workoutService.GetWorkoutsByNameAtPositionAsync(SearchText, 0);
    Workouts = workouts?.ToObservableCollection();
  }

  [RelayCommand]
  async Task Refresh()
  {
  }

  [RelayCommand]
  private async void QuickAddWorkout()
  {

  }

  [RelayCommand]
  async Task GoToCreateDiaryEntry()
  {
    await Shell.Current.GoToAsync($"{nameof(DiaryEntryWorkoutCreatePage)}", true, new Dictionary<string, object>
        {
          {"Workout", SelectedWorkout},
          {"PageMode", WorkoutDetailsVM.PageMode.View}
        });
    SelectedWorkout = null;
  }

  [RelayCommand]
  private async void NewWorkout()
  {
    await Shell.Current.GoToAsync($"{nameof(WorkoutCreatePage)}", true, new()
    {
      { "PageMode", WorkoutDetailsVM.PageMode.Create }
    });
  }
}
