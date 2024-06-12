using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPDApp.ViewModels
{
  public partial class ScannerVM : ObservableObject
  {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ButtonBarcode))]
    private string? _barcode;

    public string? ButtonBarcode => "Add " + Barcode;

    [RelayCommand]
    internal async Task GoBack() => await Shell.Current.GoToAsync($"..?Barcode={Barcode}", true);
  }
}