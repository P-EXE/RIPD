using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class BarcodeScannerZXingVM : ObservableObject
  {
    [ObservableProperty]
    private string? _barcode;

    [RelayCommand]
    internal async Task GoBack() => await Shell.Current.GoToAsync($"..",true);
  }
}
