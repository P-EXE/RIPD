using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Views.Login
{
  internal partial class LoginViewModel : ObservableObject
  {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EmailLabel))]
    private string email;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PasswordLabel))]
    private string password;

    public string EmailLabel => email;
    public string PasswordLabel => password;
  }
}
