using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Models.Settings
{
  internal class Setting
  {
    public string Title { get; set; }
    public string Subtitle { get; set; }
  }
  internal class ToggleSetting : Setting
  {
    public bool State { get; set; }
  }
}
