using Camera.MAUI.ZXingHelper;

namespace RIPD.Views;

public partial class BarcodeScannerV : ContentView
{
	public BarcodeScannerV()
	{
		
	}
    private void CamerasLoaded(object sender, EventArgs e)
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

    private void BarcodeDetected(object sender, BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {

            // barcodeResult.Text = $"{args.Result[0].BarcodeFormat} :{args.Result[0].Text} ";
        });
    }
}