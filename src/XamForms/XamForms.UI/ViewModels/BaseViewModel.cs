using ReactiveUI;

namespace XamForms.UI.ViewModels
{
  public class BaseViewModel : ReactiveObject
  {
    // [Reactive]
    public string Title { get; set; }

    // [Reactive]
    public bool IsBusy { get; set; }
  }
}