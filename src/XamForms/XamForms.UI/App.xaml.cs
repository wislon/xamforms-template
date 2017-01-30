using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Splat;
using Xamarin.Forms;
using XamForms.UI.Interfaces;
using XamForms.UI.Views;

namespace XamForms.UI
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      bool loggedIn = Task.Run(async () => await DoLoginCheck()).Result;

      if (loggedIn)
      {
        // The root page of your application
        var mdRoot = new MasterDetailRootPage();
        MainPage = mdRoot;
      }
      else
      {
        var loginPage = new LoginPage();
        var navPage = new NavigationPage(loginPage);
        MainPage = navPage;
      }

    }

    private async Task<bool> DoLoginCheck()
    {
      return await Task.FromResult(false);
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
