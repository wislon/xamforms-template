using System.Windows.Input;
using Splat;
using Xamarin.Forms;
using XamForms.UI.Interfaces;

namespace XamForms.UI.ViewModels
{
  public class NormalOneChildViewModel : BaseViewModel
  {
    public NormalOneChildViewModel()
    {
      Title = "Normal Child";
    }

    public void InitializeDisplay(string description)
    {
      Description = description;
    }

    // [Reactive]
    public string Description { get; set; }

    private ICommand _navToChild;
    public ICommand NavigatePopup
    {
      get
      {
        if (_navToChild == null)
        {
          _navToChild = new Command(async () =>
          {
            await NavService.PushModalAsync<NormalModalViewModel>();
          });
        }
        return _navToChild;
      }
    }
  }
}