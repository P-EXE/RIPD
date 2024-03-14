using Camera.MAUI;
using CommunityToolkit.Mvvm.ComponentModel;
using RIPD.ViewModels;
using System.Diagnostics;
using ZXing.Net.Maui;

namespace RIPD.Pages;

public partial class BarcodeScannerPage : ContentPage
{
  private BarcodeScannerZXingVM _vm;

  //public BarcodeScannerZXingV()
  //{
  //  Debug.WriteLine("--> Custom(Error): BarcodeScannerZXingV.Constructor: Parameterloser Constructor aufgerufen");
  //}
  private bool BarcodeCounter = false;
  public BarcodeScannerPage(BarcodeScannerZXingVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;

    BarcodeCounter = false;
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

   // Debug.WriteLine($"--> Custom(Warning): BarcodeScannerPage.BarcodesDetected: Barcodes scanned: {BarcodeCounter}");

    //BarcodeCounter++;

    ReturnToPreviousPage();

  }

  private async void ReturnToPreviousPage()
  {
    if ( BarcodeCounter )
    {
      await _vm.GoBack();
    }
    
  }
}