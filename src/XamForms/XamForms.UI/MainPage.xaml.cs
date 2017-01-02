using Xamarin.Forms;
using XamForms.Shared;

namespace XamForms.UI
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();

      var sn = new SharedNetwork();
    }
  }
}
