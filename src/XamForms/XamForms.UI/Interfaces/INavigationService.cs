using System;
using System.Threading.Tasks;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Interfaces
{
  public interface INavigationService
  {
    void RegisterViewModels(System.Reflection.Assembly asm);
    void Register(Type viewModelType, Type viewType);

    Task PopAsync();
    Task PopModalAsync();
    Task PushAsync(BaseViewModel viewModel);
    Task PushAsync<T>(Action<T> initialize = null) where T : BaseViewModel;
    Task PushModalAsync<T>(Action<T> initialize = null) where T : BaseViewModel;
    Task PushModalAsync(BaseViewModel viewModel);
    Task PopToRootAsync(bool animate);
    void SwitchDetailPage<T>(Action<T> initialize = null) where T : BaseViewModel;
    void SwitchDetailPage(BaseViewModel viewModel);
  }
}