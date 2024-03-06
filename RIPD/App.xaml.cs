using RIPD.Pages;

namespace RIPD
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      MainPage = new AppShell();
    }
  }
}
