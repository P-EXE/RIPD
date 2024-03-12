using Camera.MAUI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class BarcodeScannerVM : ObservableObject
  {
    [ObservableProperty]
    private string _barcode;

    internal async Task GoBack() => await Shell.Current.GoToAsync($"..?barcode={Barcode}");
  }
}
