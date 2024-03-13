using Camera.MAUI;
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
  private int BarcodeCounter = 0;
  public BarcodeScannerPage(BarcodeScannerZXingVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;

    BarcodeCounter = 0;
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
    if (BarcodeCounter == 0)
    {
      _vm.Barcode = e.Results?.FirstOrDefault().Value;
     
      _vm.GoBack();
    }
    Debug.WriteLine($"--> Custom(Warning): BarcodeScannerPage.BarcodesDetected: Barcodes scanned: {BarcodeCounter}");
    BarcodeCounter++;
    
    
    
  }
}