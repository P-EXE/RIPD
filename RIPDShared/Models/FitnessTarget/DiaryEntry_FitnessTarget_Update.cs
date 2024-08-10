using CommunityToolkit.Mvvm.ComponentModel;

namespace RIPDShared.Models;

[INotifyPropertyChanged]
public partial class DiaryEntry_FitnessTarget_Update : DiaryEntry_Update
{
  [ObservableProperty]
  private double _weight;
  [ObservableProperty]
  private DateTime _targetDateTime;
}
