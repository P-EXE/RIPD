using CommunityToolkit.Mvvm.ComponentModel;

namespace RIPDApp.ViewModels;

public partial class DiaryVM : ObservableObject
{
  public DiaryVM()
  {
    
  }

  [ObservableProperty]
  private DateOnly _date;
}
