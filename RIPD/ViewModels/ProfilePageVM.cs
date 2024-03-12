using RIPD.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  class ProfilePageVM
  {
    private readonly LocalDBContext _localDBContext;

    public ProfilePageVM(LocalDBContext localDBContext)
    {
      _localDBContext = localDBContext;
    }
  }
}
