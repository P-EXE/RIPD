using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class BarcodeScannerZXingVM : ObservableObject
  {
    [ObservableProperty]
    private string? _barcodeCode;

  }
}
