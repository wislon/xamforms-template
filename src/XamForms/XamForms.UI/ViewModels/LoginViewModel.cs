using ReactiveUI.Fody.Helpers;

namespace XamForms.UI.ViewModels
{
  public class LoginViewModel : BaseViewModel
  {
    [Reactive]
    public string MainText { get; set; }

    public LoginViewModel()
    {
      Title = "Login";
      MainText = "This is my login. There are many like it, but this one is mine";
    }

  }
}