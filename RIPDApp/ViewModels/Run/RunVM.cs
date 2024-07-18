using RIPDApp.Services.Run;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPDApp.ViewModels.Run
{
  class RunVM : RunGpsLocation
  {
    public RunVM()
    {
      
    }

    public async void Test()
    {
      await GetCurrentLocation();
    }
  }
}
