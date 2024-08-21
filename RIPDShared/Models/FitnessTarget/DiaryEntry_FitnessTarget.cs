using CommunityToolkit.Mvvm.ComponentModel;

namespace RIPDShared.Models;

[INotifyPropertyChanged]
public partial class DiaryEntry_FitnessTarget : DiaryEntry
{
  [ObservableProperty]
  private double _weight;
  [ObservableProperty]
  private DateTime _targetDateTime;
}