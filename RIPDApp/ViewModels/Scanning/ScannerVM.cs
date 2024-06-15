using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RIPDApp.Messaging;

namespace RIPDApp.ViewModels
{
  public partial class ScannerVM : ObservableObject
  {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ButtonBarcode))]
    private string? _barcode;

    public string? ButtonBarcode => "Return " + Barcode;

    [RelayCommand]
    internal async Task ReturnScanResult()
    {
      WeakReferenceMessenger.Default.Send(new PageReturnObjectMessage<string>(Barcode));
      await Shell.Current.GoToAsync($"..?Barcode={Barcode}", true);
    }
  }
}