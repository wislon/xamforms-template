using ReactiveUI;
using Splat;
using XamForms.UI.Interfaces;

namespace XamForms.UI.ViewModels
{
  public class BaseViewModel : ReactiveObject
  {

    protected readonly INavigationService NavService;

    // [Reactive]
    public string Title { get; set; }

    // [Reactive]
    public bool IsBusy { get; set; }

    public BaseViewModel()
    {
      NavService = Locator.CurrentMutable.GetService<INavigationService>();
    }
  }
}