using Camera.MAUI;
using RIPD.ViewModels;

namespace RIPD.Views;

public partial class BarcodeScannerV : ContentView
{
  private BarcodeScannerVM _vm;
  public BarcodeScannerV()
  {
    InitializeComponent();
    BindingContext = this;
  }
  public BarcodeScannerV(BarcodeScannerVM vm)
  {
    InitializeComponent();
    _vm = vm;
    BindingContext = _vm;
  }

  private void cameraView_CamerasLoaded(object sender, EventArgs e)
  {
    if (cameraView.Cameras.Count > 0)
    {
      cameraView.Camera = cameraView.Cameras.First();
      MainThread.BeginInvokeOnMainThread(async () =>
      {
        await cameraView.StopCameraAsync();
        await cameraView.StartCameraAsync();
      });
    }
  }

  private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
  {
    MainThread.BeginInvokeOnMainThread(() =>
    {
      barcodeResult.Text = $"{args.Result[0].BarcodeFormat} :{args.Result[0].Text} ";
    });
    /*_vm.GoBack();*/
  }
}