using System.Windows.Input;
using Splat;
using Xamarin.Forms;

namespace XamForms.UI.ViewModels
{
  public class NormalOneViewModel : BaseViewModel
  {

    //[Reactive]
    public string Description { get; set; }

    private ICommand _navToChild;

    public ICommand NavigateToChild
    {
      get
      {
        if (_navToChild == null)
        {
          _navToChild = new Command(async () =>
            {
              await NavService.PushAsync<NormalOneChildViewModel>((vm) => vm.InitializeDisplay("Normal child!"));
            }
          );
        }
        return _navToChild;
      }
    }

    public NormalOneViewModel()
    {
      Title = "Normal";
      Description = "Normal navigation stack only";
    }

  }
}
 