using RIPDApp.ViewModels;
using ZXing.Net.Maui;

namespace RIPDApp.Pages;

public partial class BarcodeScannerPage : ContentPage
{
  private readonly ScannerVM _vm;
  private bool _barcodeDetected = false;

  public BarcodeScannerPage(ScannerVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;

    barcodeReaderZXing.BindingContext = this;
    barcodeReaderZXing.Options = new BarcodeReaderOptions()
    {
      AutoRotate = true,
      Multiple = false
    };
    barcodeReaderZXing.CameraLocation = CameraLocation.Rear;
  }

  private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
  {
    _vm.Barcode = e.Results?.FirstOrDefault().Value;
    ReturnToPreviousPage();
  }

  private async void ReturnToPreviousPage()
  {
    if (_barcodeDetected)
    {
      await _vm.GoBack();
    }
  }
}