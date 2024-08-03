using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RIPDApp.Collections;
using RIPDApp.Messaging;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

public partial class WorkoutDetailsVM : ObservableObject
{
  private readonly IWorkoutService _workoutService;
  public WorkoutDetailsVM(IWorkoutService workoutService)
  {
    _workoutService = workoutService;
  }

  // Page State Fields
  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  private bool _pageModeView;
  [ObservableProperty]
  private bool _pageModeUpdate;
  [ObservableProperty]
  private bool _pageModeCreate;

  // Availability
  [ObservableProperty]
  private bool _available;

  // Displayed Fields
  [ObservableProperty]
  private Workout? _workout = new();

  // Experimental
  [ObservableProperty]
  ObservablePropertyCollection<Workout> _workoutProperties = [];

  [ObservableProperty]
  private DateTime _acted = DateTime.Now;

  [RelayCommand]
  private async Task CreateWorkout()
  {
    try
    {
      Workout = await _workoutService.CreateWorkoutAsync(Workout);
    }
    catch (Exception ex)
    {
      await Shell.Current.DisplayAlert("Error", ex.Message, "Return");
    }
    await GoBack();
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");

  public enum PageMode
  {
    Default = 0,
    View = 1,
    Update = 2,
    Create = 3,
  }
}
