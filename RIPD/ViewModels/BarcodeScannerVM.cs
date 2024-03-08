using Camera.MAUI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.Pages;
using RIPD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camera.MAUI.ZXingHelper;

namespace RIPD.ViewModels
{
    internal partial class BarcodeScannerVM : ObservableObject
    {
        private void CamerasLoaded(object cv, EventArgs e)
        {
            CameraView cameraView = cv as CameraView;
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
}
