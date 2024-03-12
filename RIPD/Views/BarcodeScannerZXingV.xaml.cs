using RIPD.ViewModels;
using ZXing.Net.Maui;
using ZXing.OneD;

namespace RIPD.Views;

public partial class BarcodeScannerZXingV : ContentView
{
  private BarcodeScannerZXingVM _vm;
  public BarcodeScannerZXingV()
  {
    InitializeComponent();
    _vm = new BarcodeScannerZXingVM();
    BindingContext = _vm;

    barcodeReaderZXing.BindingContext = this;
    barcodeReaderZXing.Options = new BarcodeReaderOptions()
    {
      AutoRotate = true,
      Multiple = false,
      Formats = BarcodeFormat.Ean13
    };
    barcodeReaderZXing.CameraLocation = CameraLocation.Rear;
  }
  public BarcodeScannerZXingV(BarcodeScannerZXingVM vm)
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
    _vm.BarcodeCode = e.Results?.FirstOrDefault().Value;
  }
}