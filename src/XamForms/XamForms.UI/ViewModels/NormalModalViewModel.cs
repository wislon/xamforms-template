using System.Windows.Input;
using Splat;
using Xamarin.Forms;
using XamForms.UI.Interfaces;

namespace XamForms.UI.ViewModels
{
  public class NormalModalViewModel : BaseViewModel
  {

    public NormalModalViewModel()
    {
      Title = "Normal Modal";
    }

    private ICommand _dismissModal;

    public ICommand DismissModalCommand
    {
      get
      {
        if (_dismissModal == null)
        {
          _dismissModal = new Command(async () => await NavService.PopModalAsync());
        }
        return _dismissModal;
      }
    }
  }
}