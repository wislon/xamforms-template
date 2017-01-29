using Xamarin.Forms;

namespace XamForms.UI
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      // The root page of your application
      var mdRoot = new MasterDetailRootPage();
      MainPage = mdRoot;
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
